﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pladeco.Model;
using Pladeco.Web.Data;
using Pladeco.Web.Models;

namespace Pladeco.Web.Controllers
{
    public class PlanTasksController : Controller
    {
        private readonly ApplicationDbContext context;

        public PlanTasksController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Create(int id)
        {
            var plan = context.Plans
                .Include(p=>p.Project)
                .Where(p => p.ID == id).FirstOrDefault();

            var model = ToPlanTaskViewModel(new PlanTask(plan));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanTaskViewModel view)
        {
            if (ModelState.IsValid)
            {
                PlanTask planTask = this.ToPlanTask(view);

                planTask.ID = 0;

                context.Add(planTask);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { planTask.ID });
            }


            return View(view);
        }

        private PlanTask ToPlanTask(PlanTaskViewModel view)
        {
            return new PlanTask
            {
                ID = view.ID,
                Name = view.Name,
                Description = view.Description,
                Priority = view.Priority,
                PlanID = view.PlanID,
                ResponsableID = view.ResponsableID,
                StartDate = view.StartDate,
                EndDate = view.EndDate
            };
        }

        private PlanTaskViewModel ToPlanTaskViewModel(PlanTask planTask)
        {
            var model = new PlanTaskViewModel()
            {
                ID = planTask.ID,
                Name = planTask.Name,
                Description = planTask.Description,
                Priority = planTask.Priority,

                StartDate = planTask.StartDate,
                EndDate = planTask.EndDate,

                Users = new SelectList(context.Users, "Id", "Name", planTask.ResponsableID),

                Plan = planTask.Plan,
                PlanID = planTask.PlanID

            };

            return model;
        }

        public async Task<IActionResult> Details(int id)
        {

            var planTask = await context.Tasks
                .Include(p => p.Plan)
                    .ThenInclude(p=> p.Project)
                .Where(p => p.ID == id)
                .FirstOrDefaultAsync();
            if (planTask == null)
            {
                return NotFound();
            }

            return View(planTask);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planTask = await context.Tasks
                .Include(p => p.Responsable)
                .Include(p => p.Plan)
                    .ThenInclude(p => p.Project)
                .Where(b => b.ID == id)
                .FirstOrDefaultAsync();

            if (planTask == null)
            {
                return NotFound();
            }

            var model = ToPlanTaskViewModel(planTask);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlanTaskViewModel view)
        {
            if (id != view.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    PlanTask planTask = this.ToPlanTask(view);

                    context.Update(planTask);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanTaskExists(view.ID))
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

        private bool PlanTaskExists(int id)
        {
            return context.Tasks.Any(e => e.ID == id);
        }
    }
}