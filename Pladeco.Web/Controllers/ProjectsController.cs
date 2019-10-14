﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pladeco.Model;
using Pladeco.Model.Enum;
using Pladeco.Web.Data;
using Pladeco.Web.Models;

namespace Pladeco.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProjectsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.Projects.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await context.Projects
                .Include(p=> p.Area)
                .Include(p => p.Responsable)
                .Include(p => p.Solicitante)
                .Where(p=> p.ID==id)
                .FirstOrDefaultAsync();
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        public IActionResult Create()
        {
            var model = new ProjectViewModel()
            {
                Areas= new SelectList(context.Areas, "ID", "Name"),
                Users= new SelectList(context.Users, "Id", "Name")

            };
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
                StartDate = view.StartDate,
                EndDate = view.EndDate,
                RealStartDate = view.RealStartDate,
                RealEndDate = view.RealEndDate
            };
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await context.Projects
                .Where(b => b.ID == id)
                .FirstOrDefaultAsync();

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Amount,AreaID,ID")] Project project)
        {
            if (id != project.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(project);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ID))
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

            return View(project);
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
        private bool ProjectExists(int id)
        {
            return context.Projects.Any(e => e.ID == id);
        }
    }
}