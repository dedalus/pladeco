using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pladeco.Model;
using Pladeco.Web.Data;
using Pladeco.Web.Helpers;
using Pladeco.Web.Models;

namespace Pladeco.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BudgetsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICombosHelper combosHelper;

        public BudgetsController(ApplicationDbContext context, ICombosHelper combosHelper)
        {
            this.context = context;
            this.combosHelper = combosHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.Budgets
                .Include(b=> b.Area)
                .OrderByDescending(b=> b.Year)
                .ToListAsync());
        }

        public IActionResult Create()
        {

            var model = ToBudgetViewModel(new Budget());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BudgetViewModel view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await ValidateBudget(view))
                    {
                        Budget budget = this.ToBudget(view);

                        budget.ID = 0;

                        context.Add(budget);
                        await context.SaveChangesAsync();
                        return RedirectToAction(nameof(Details), new { budget.ID });
                    }
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }

            view.Areas = combosHelper.GetComboAreas();

            return View(view);
        }

        private Budget ToBudget(BudgetViewModel view)
        {
            return new Budget
            {
                ID = view.ID,
                AreaID= view.AreaID,
                Year = view.Year,
                Amount = view.Amount
            };
        }

        private async Task<bool> ValidateBudget(BudgetViewModel view)
        {
            var budget = await context.Budgets.Where(b => b.AreaID == view.AreaID && b.Year == view.Year).FirstOrDefaultAsync();

            if (budget != null)
            {
                throw new Exception($"Ya se asignó un presupuesto a esta área para el año {budget.Year}");
            }

            return true;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await context.Budgets
                .Include(b=> b.Area)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (area == null)
            {
                return NotFound();
            }

            var model = ToBudgetViewModel(area);

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await context.Budgets
                .Include(b=> b.Area)
                .Where(b=> b.ID==id)
                .FirstOrDefaultAsync();

            if (budget == null)
            {
                return NotFound();
            }

            var model = ToBudgetViewModel(budget);
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BudgetViewModel view)
        {
            if (id != view.AreaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (await ValidateBudget(view))
                    {
                        Budget budget = ToBudget(view);

                        context.Update(budget);
                        await context.SaveChangesAsync();
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetExists(view.AreaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                return RedirectToAction(nameof(Details), new { id });
            }

            view.Areas = combosHelper.GetComboAreas();
            return View(view);
        }

        private BudgetViewModel ToBudgetViewModel(Budget budget)
        {
            var model = new BudgetViewModel()
            {
                ID = budget.ID,
                AreaID = budget.AreaID,
                Amount = budget.Amount,
                Year = budget.Year,
                Areas = combosHelper.GetComboAreas()
                
            };

            if (budget.Area != null)
            {
                model.AreaName = budget.Area.Name;
            }

            return model;
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budget = await context.Budgets.FindAsync(id);
            context.Budgets.Remove(budget);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetExists(int id)
        {
            return context.Budgets.Any(e => e.ID == id);
        }
    }
}