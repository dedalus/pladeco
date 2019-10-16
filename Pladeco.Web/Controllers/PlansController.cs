using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pladeco.Web.Data;

namespace Pladeco.Web.Controllers
{
    public class PlansController : Controller
    {
        private readonly ApplicationDbContext context;

        public PlansController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index(int id)
        {
            var plan = context.Plans
                .Include(p => p.Project)
                    .ThenInclude(p=> p.Area)
                .Include(p => p.Tasks)
                .Where(p => p.ID == id)
                .FirstOrDefault();

            return View();
        }
    }
}