using System;
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
    public class BudgetsController : Controller
    {
        private readonly ApplicationDbContext context;

        public BudgetsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.Areas.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await context.Areas
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

            var area = await context.Areas.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            var model = ToBudgetViewModel(area);
           
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
                    Area area = await context.Areas.FindAsync(view.AreaID);
                    area.Budget = view.Amount;

                    context.Update(area);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaExists(view.AreaID))
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

        private BudgetViewModel ToBudgetViewModel(Area area)
        {
            var model = new BudgetViewModel()
            {
                AreaID = area.ID,
                AreaName=area.Name
            };

            if (area.Budget == null)
            {
                model.Amount = 0;
            }
            else
            {
                model.Amount = (decimal)area.Budget;
            }
            
            return model;
        }

        private bool AreaExists(int id)
        {
            return context.Areas.Any(e => e.ID == id);
        }
    }
}