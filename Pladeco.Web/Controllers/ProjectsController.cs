﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
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

            int project_done = 0;
            int project_in_process = 0;
            int project_pending = 0;

            foreach (var item in project.Plans)
            {
                int done =(from t in item.Tasks where t.Status == eStatus.DONE select t).Count();
                int in_process = (from t in item.Tasks where t.Status == eStatus.IN_PROCESS select t).Count();
                int pending = (from t in item.Tasks where t.Status == eStatus.PENDING select t).Count();

                project_done += done;
                project_in_process += in_process;
                project_pending += pending;

                if (done==0 && in_process == 0)
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

            int total = (project_done + project_in_process + project_pending);


            project.Porc = total== 0 ? 0: (project_done * 100) / total;

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

            var project = await context.Projects
                .Include(p=> p.ResponsableBudget)
                .Include(p => p.PaymentPlans)
                    .ThenInclude(p=> p.Solicitante)
                .Where(p => p.ID == id)
                .FirstOrDefaultAsync();
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
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
                Stages = combosHelper.GetComboStages(project.TypologyID),

                Colaborators = new SelectList(context.Users, "Id", "Name")
                //SelectedUsers = project.Colaborators.Select(sc => sc.UserID)

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
                .Include(p=> p.Colaborators)
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

        public async Task<IActionResult> Budget(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await context.Projects
                .Include(p => p.Area)
                .Where(p => p.ID == id)
                .FirstOrDefaultAsync();

            if (project == null)
            {
                return NotFound();
            }

            var model = ToProjectBudgetViewModel(project);

            return View(model);
        }

        private ProjectBudgetViewModel ToProjectBudgetViewModel(Project project)
        {
            var model = new ProjectBudgetViewModel()
            {
                ProjectID = project.ID,
                ProjectName = project.Name,
                AreaID=project.AreaID,
                BudgetDescription=project.BudgetDescription,
                ResponsableBudgetID = project.ResponsableBudgetID,
                Users = combosHelper.GetComboUsers()

            };

            if (project.BudgetAmount == null)
            {
                model.Amount = 0;
            }
            else
            {
                model.Amount = (decimal)project.BudgetAmount;
            }

            return model;
        }

        private async Task<bool> CheckBudget(ProjectBudgetViewModel view)
        {
            Area area = await context.Areas
                .Include(p => p.Projects)
                .Where(p => p.ID == view.AreaID)
                .FirstOrDefaultAsync();

            decimal budget = (decimal)area.Projects
                .Select(p => p.BudgetAmount)
                .DefaultIfEmpty()
                .Sum();


            if (budget + view.Amount <= area.Budget)
            {
                return true;
            }
            else
            {
                throw new Exception($"Presupuesto excedido. El monto asigado al area es de ${area.Budget}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Budget(int id, ProjectBudgetViewModel view)
        {
            if (id != view.ProjectID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(await CheckBudget(view))
                    {
                        Project project = await context.Projects.FindAsync(view.ProjectID);
                        project.BudgetAmount = view.Amount;
                        project.BudgetDescription = view.BudgetDescription;
                        project.ResponsableBudgetID = view.ResponsableBudgetID;

                        context.Update(project);
                        await context.SaveChangesAsync();

                        return RedirectToAction(nameof(PaymentPlan), new { id });
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(view.ProjectID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                
            }

            return View(view);
        }

        private bool ProjectExists(int id)
        {
            return context.Projects.Any(e => e.ID == id);
        }
    }
}