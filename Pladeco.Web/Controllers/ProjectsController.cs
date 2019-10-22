using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pladeco.Model;
using Pladeco.Model.Enum;
using Pladeco.Web.Data;
using Pladeco.Web.Helpers;
using Pladeco.Web.Models;

namespace Pladeco.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICombosHelper combosHelper;

        public ProjectsController(ApplicationDbContext context,ICombosHelper combosHelper)
        {
            this.context = context;
            this.combosHelper = combosHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.Projects
                .Include(p=> p.ResponsableUnit)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await context.Projects
                .Include(p=> p.Area)
                .Include(p => p.ResponsableUnit)
                .Include(p => p.Sector)
                .Include(p => p.DevAxis)
                .Include(p => p.Responsable)
                .Include(p => p.Solicitante)
                .Include(p => p.Typology)
                .Include(p => p.Stage)
                .Include(p=> p.Plans)
                    .ThenInclude(p=> p.Tasks)
                .Where(p=> p.ID==id)
                .FirstOrDefaultAsync();

            foreach (var item in project.Plans)
            {
                int done =(from t in item.Tasks where t.Status == eStatus.DONE select t).Count();
                int in_process = (from t in item.Tasks where t.Status == eStatus.IN_PROCESS select t).Count();
                int pending = (from t in item.Tasks where t.Status == eStatus.PENDING select t).Count();

                if(done==0 && in_process == 0)
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

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        public async Task<IActionResult> PaymentPlan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await context.Projects
                .Include(p => p.PaymentPlans)
                    .ThenInclude(p=> p.Solicitante)
                .Where(p => p.ID == id)
                .FirstOrDefaultAsync();
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        public IActionResult Create()
        {
            //var model = new ProjectViewModel()
            //{
            //    Areas= new SelectList(context.Areas, "ID", "Name"),
            //    Users= new SelectList(context.Users, "Id", "Name")

            //};
            var model = ToProjectViewModel(new Project());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectViewModel view)
        {
            if (ModelState.IsValid)
            {
                Project project = this.ToProject(view);

                context.Add(project);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { project.ID });
            }


            return View(view);
        }

        private Project ToProject(ProjectViewModel view)
        {
            return new Project
            {
                ID = view.ID,
                Name = view.Name,
                Description = view.Description,
                Priority = view.Priority,
                AreaID = view.AreaID,
                SolicitanteID = view.SolicitanteID,
                ResponsableID = view.ResponsableID,
                SectorID=view.SectorID,
                ResponsableUnitID=view.ResponsableUnitID,
                DevAxisID=view.DevAxisID,
                TypologyID = view.TypologyID,
                StageID = view.StageID,
                StartDate = view.StartDate,
                EndDate = view.EndDate,
                RealStartDate = view.RealStartDate,
                RealEndDate = view.RealEndDate
            };
        }

        private ProjectViewModel ToProjectViewModel(Project project)
        {
            var model = new ProjectViewModel()
            {
                ID=project.ID,
                Name=project.Name,
                Description=project.Description,
                Priority=project.Priority,
                
                StartDate=project.StartDate,
                EndDate=project.EndDate,
                RealStartDate=project.RealStartDate,
                RealEndDate=project.RealEndDate,

                AreaID= project.AreaID,
                Areas = combosHelper.GetComboAreas(),

                ResponsableID = project.ResponsableID,
                SolicitanteID = project.SolicitanteID,
                Users = combosHelper.GetComboUsers(),

                DevAxisID = project.DevAxisID,
                DevAxes = combosHelper.GetComboDevAxes(),

                ResponsableUnitID = project.ResponsableUnitID,
                ResponsableUnits = combosHelper.GetComboResponsableUnits(),

                SectorID = project.SectorID,
                Sectors = combosHelper.GetComboSectors(),

                TypologyID = project.TypologyID,
                Typologies = combosHelper.GetComboTypologies(),

                StageID = project.StageID,
                Stages = combosHelper.GetComboStages(0)

            };


            //model.Areas.Append(new SelectListItem()
            //{
            //    Text="Select",
            //    Value="0"
            //});

            return model;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await context.Projects
                .Include(p => p.Area)
                .Include(p=> p.ResponsableUnit)
                .Include(p=> p.Sector)
                .Include(p=> p.DevAxis)
                .Include(p => p.Responsable)
                .Include(p => p.Solicitante)
                .Include(p => p.Typology)
                .Include(p => p.Stage)
                .Where(b => b.ID == id)
                .FirstOrDefaultAsync();

            if (project == null)
            {
                return NotFound();
            }

            var model = ToProjectViewModel(project);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectViewModel view)
        {
            if (id != view.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Project project = this.ToProject(view);

                    context.Update(project);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(view.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id });
            }

            return View(view);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await context.Projects.FindAsync(id);
            context.Projects.Remove(project);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> GetStagesAsync(int typologyID)
        {  
            var typology = await context.Typologies.Include(t => t.Stages).Where(t=> t.ID==typologyID).FirstOrDefaultAsync();
            return this.Json(typology.Stages.OrderBy(c => c.Name).ToList());
        }

        private bool ProjectExists(int id)
        {
            return context.Projects.Any(e => e.ID == id);
        }
    }
}