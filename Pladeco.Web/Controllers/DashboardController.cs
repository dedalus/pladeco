using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pladeco.Model;
using Pladeco.Model.Enum;
using Pladeco.Web.Data;

namespace Pladeco.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext context;

        public DashboardController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Project> projects = await context.Projects
                .Include(p => p.Area)
                .Include(p => p.Responsable)
                .Include(p => p.Plans)
                    .ThenInclude(p => p.Tasks)
                .ToListAsync();

            foreach (var p in projects)
            {
                int project_done = 0;
                int project_in_process = 0;
                int project_pending = 0;

                foreach (var item in p.Plans)
                {
                    int done = (from t in item.Tasks where t.Status == eStatus.DONE select t).Count();
                    int in_process = (from t in item.Tasks where t.Status == eStatus.IN_PROCESS select t).Count();
                    int pending = (from t in item.Tasks where t.Status == eStatus.PENDING select t).Count();

                    project_done += done;
                    project_in_process += in_process;
                    project_pending += pending;

                    if (done == 0 && in_process == 0)
                    {
                        item.Status = eStatus.PENDING;
                        item.Porc = 0;
                    }
                    else
                    {
                        if (in_process == 0)
                        {
                            item.Status = eStatus.DONE;
                            item.Porc = 100;
                        }
                        else
                        {
                            item.Status = eStatus.IN_PROCESS;
                            item.Porc = (done * 100) / (done + in_process + pending);
                        }
                    }

                }
                p.DoneTasks = project_done;
                p.TotalTasks = project_done + project_in_process + project_pending;


                p.Porc = p.TotalTasks==0 ? 0: (project_done * 100) / p.TotalTasks;
            }
            
            return View(projects);
        }
    }
}