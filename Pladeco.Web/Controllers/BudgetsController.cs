using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pladeco.Model;
using Pladeco.Web.Data;

namespace Pladeco.Web.Controllers
{
    public class BudgetsController : Controller
    {
        private readonly ApplicationDbContext context;

        public BudgetsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.Budgets.Include(b => b.Area).ToListAsync());
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

            return View(area);
        }

        public IActionResult Create()
        {
            ViewData["area_id"] = new SelectList(context.Areas, "ID", "Name");

            return View(new Budget());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Amount,AreaID")] Budget bugdet)
        {
            if (ModelState.IsValid)
            {

                context.Add(bugdet);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { bugdet.ID });
            }

            ViewData["area_id"] = new SelectList(context.Areas, "ID", "Name", bugdet.Area);

            return View(bugdet);
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
            ViewData["area_id"] = new SelectList(context.Areas, "ID", "Name", budget.AreaID);

            return View(budget);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Amount,AreaID,ID")] Budget budget)
        {
            if (id != budget.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(budget);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetExists(budget.ID))
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
            ViewData["area_id"] = new SelectList(context.Areas, "ID", "Name", budget.Area);
            return View(budget);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await context.Budgets.FindAsync(id);
            context.Budgets.Remove(product);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool BudgetExists(int id)
        {
            return context.Budgets.Any(e => e.ID == id);
        }
    }
}