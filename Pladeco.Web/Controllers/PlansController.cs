using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pladeco.Model;
using Pladeco.Web.Data;
using Pladeco.Web.Helpers;
using Pladeco.Web.Models;

namespace Pladeco.Web.Controllers
{
    public class PlansController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICombosHelper combosHelper;

        public PlansController(ApplicationDbContext context,ICombosHelper combosHelper)
        {
            this.context = context;
            this.combosHelper = combosHelper;
        }
        public async Task<IActionResult> Details(int id)
        {

            var area = await context.Plans
                .Include(p => p.Tasks)
                .Include(p => p.Project)
                .Where(p => p.ID == id)
                .FirstOrDefaultAsync();
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        public IActionResult Create(int id)
        {
            var project = context.Projects.Where(p => p.ID == id).FirstOrDefault();

            var model = ToPlanViewModel(new Plan(project));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanViewModel view)
        {
            if (ModelState.IsValid)
            {
                Plan plan = this.ToPlan(view);

                plan.ID = 0;

                context.Add(plan);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { plan.ID });
            }


            return View(view);
        }

        private Plan ToPlan(PlanViewModel view)
        {
            return new Plan
            {
                ID = view.ID,
                Name=view.Name,
                Description = view.Description,
                Priority = view.Priority,
                ProjectID = view.ProjectID,
                ResponsableID=view.ResponsableID,
                StartDate = view.StartDate,
                EndDate = view.EndDate,
                RealStartDate = view.RealStartDate,
                RealEndDate = view.RealEndDate
            };
        }

        private PlanViewModel ToPlanViewModel(Plan plan)
        {
            var model = new PlanViewModel()
            {
                ID = plan.ID,
                Name=plan.Name,
                Description = plan.Description,
                Priority = plan.Priority,

                StartDate = plan.StartDate,
                EndDate = plan.EndDate,
                RealStartDate = plan.RealStartDate,
                RealEndDate = plan.RealEndDate,

                ResponsableID = plan.ResponsableID,
                Users = combosHelper.GetComboUsers(),

                Project =plan.Project,
                ProjectID=plan.ProjectID

            };

            return model;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await context.Plans
                .Include(p=> p.Responsable)
                .Include(p=>p.Project)
                .Where(b => b.ID == id)
                .FirstOrDefaultAsync();

            if (plan == null)
            {
                return NotFound();
            }

            var model = ToPlanViewModel(plan);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlanViewModel view)
        {
            if (id != view.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Plan plan = this.ToPlan(view);

                    context.Update(plan);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(view.ID))
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

        private bool PlanExists(int id)
        {
            return context.Plans.Any(e => e.ID == id);
        }

       
    }
}