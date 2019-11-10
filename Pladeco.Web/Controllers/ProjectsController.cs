using System;
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
using Pladeco.Web.Data.Data;
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
                .Include(p=> p.Colaborators)
                    .ThenInclude(p=> p.User)
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

                if (pending == 0 && in_process == 0)
                {
                    item.Status = eStatus.DONE;
                    item.Porc = 100;
                }
                else
                {
                    if (done == 0)
                    {
                        item.Status = eStatus.PENDING;
                        item.Porc = 0;
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

            var model = ToProjectViewModel(project);

            return View(model);
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
                Project project = await ToProject(view);

                IEnumerable<ProjectUser> colaboratos = new List<ProjectUser>();

                if (view.SelectedUsers != null)
                {
                    colaboratos = view.SelectedUsers.Select(p => new ProjectUser
                    {
                        ProjectID = project.ID,
                        UserID = p
                    });
                }

                project.Colaborators.AddRange(
                    colaboratos.Where(nu => !project.Colaborators.Contains(nu)));

                context.Add(project);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { project.ID });
            }


            return View(view);
        }

        private async Task<Project> ToProject(ProjectViewModel view)
        {
            var project = await context.Projects
                .Include(p => p.Area)
                .Include(p => p.ResponsableUnit)
                .Include(p => p.Sector)
                .Include(p => p.DevAxis)
                .Include(p => p.Responsable)
                .Include(p => p.Solicitante)
                .Include(p => p.Typology)
                .Include(p => p.Stage)
                .Include(p => p.Plans)
                    .ThenInclude(p => p.Tasks)
                .Include(p => p.Colaborators)
                    .ThenInclude(p => p.User)
                .Where(p => p.ID == view.ID)
                .FirstOrDefaultAsync();

            if (project == null)
            {
                project = new Project();
            }

            project.Name = view.Name;
            project.Description = view.Description;
            project.Priority = view.Priority;
            project.AreaID = view.AreaID;
            project.SolicitanteID = view.SolicitanteID;
            project.ResponsableID = view.ResponsableID;
            project.SectorID = view.SectorID;
            project.ResponsableUnitID = view.ResponsableUnitID;
            project.DevAxisID = view.DevAxisID;
            project.TypologyID = view.TypologyID;
            project.StageID = view.StageID;
            project.StartDate = view.StartDate;
            project.EndDate = view.EndDate;
            project.RealStartDate = view.RealStartDate;
            project.RealEndDate = view.RealEndDate;

            return project;
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

                ColaboratorsList = combosHelper.GetComboUsers(),
                SelectedUsers = project.Colaborators == null ? null : project.Colaborators.Select(sc => sc.UserID),

                Plans=project.Plans

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
                    Project project = await ToProject(view);

                    //IEnumerable<ProjectUser> colaboratos = new List<ProjectUser>();

                    //if (view.SelectedUsers != null)
                    //{
                    //    colaboratos = view.SelectedUsers.Select(p => new ProjectUser
                    //    {
                    //        ProjectID = project.ID,
                    //        UserID = p
                    //    });
                    //}

                    if (view.SelectedUsers == null)
                    {
                        project.Colaborators.Clear();
                    }
                    else
                    {
                        context.TryUpdateManyToMany(project.Colaborators, view.SelectedUsers
                            .Select(x => new ProjectUser
                            {
                                UserID = x,
                                ProjectID = project.ID
                            }), x => x.UserID);
                    }
                    


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

            decimal budgetAmount = (decimal)area.Projects
                .Select(p => p.BudgetAmount)
                .DefaultIfEmpty()
                .Sum();

            var budget = await context.Budgets
                .Where(b => b.Year == DateTime.Now.Year && b.AreaID == area.ID)
                .FirstOrDefaultAsync();

            if (budget == null)
            {
                throw new Exception($"No hay presupuesto asignado en el área para este año.");
            }

            if (budgetAmount + view.Amount <= budget.Amount)
            {
                return true;
            }
            else
            {
                throw new Exception($"Presupuesto excedido. El monto asigado al area es de ${budget.Amount}");
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

            view.Users = combosHelper.GetComboUsers();

            return View(view);
        }

        private bool ProjectExists(int id)
        {
            return context.Projects.Any(e => e.ID == id);
        }
    }
}