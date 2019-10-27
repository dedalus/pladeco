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
    public class PaymentPlansController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICombosHelper combosHelper;

        public PaymentPlansController(ApplicationDbContext context,ICombosHelper combosHelper)
        {
            this.context = context;
            this.combosHelper = combosHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {

            var paymentPlan = await context.PaymentPlans
                .Include(p => p.Project)
                .Include(p=> p.Solicitante)
                .Where(p => p.ID == id)
                .FirstOrDefaultAsync();
            if (paymentPlan == null)
            {
                return NotFound();
            }

            return View(paymentPlan);
        }

        public IActionResult Create(int id)
        {
            var project = context.Projects.Where(p => p.ID == id).FirstOrDefault();

            var model = ToPaymentPlanViewModel(new PaymentPlan(project));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentPlanViewModel view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await CheckBudget(view))
                    {
                        PaymentPlan paymentPlan = this.ToPaymentPlan(view);

                        paymentPlan.ID = 0;

                        context.Add(paymentPlan);
                        await context.SaveChangesAsync();
                        return RedirectToAction(nameof(Details), new { paymentPlan.ID });
                    }
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }

            return View(view);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentPlan = await context.PaymentPlans
                .Include(p=> p.Project)
                .Include(p=> p.Solicitante)
                .Where(b => b.ID == id)
                .FirstOrDefaultAsync();

            if (paymentPlan == null)
            {
                return NotFound();
            }

            var model = ToPaymentPlanViewModel(paymentPlan);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PaymentPlanViewModel view)
        {
            if (id != view.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    PaymentPlan paymentPlan = this.ToPaymentPlan(view);

                    context.Update(paymentPlan);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentPlanExists(view.ID))
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
            var paymentPlans = await context.PaymentPlans.FindAsync(id);
            context.PaymentPlans.Remove(paymentPlans);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task<bool> CheckBudget(PaymentPlanViewModel view)
        {
            Project project = await context.Projects
                .Include(p => p.PaymentPlans)
                .Where(p => p.ID == view.ProjectID)
                .FirstOrDefaultAsync();

            decimal budget = project.PaymentPlans.Sum(p => p.Amount);

            if(budget + view.Amount <= project.BudgetAmount)
            {
                return true;
            }
            else
            {
                throw new Exception($"Presupuesto excedido. El monto asigado al proyecto es de ${project.BudgetAmount}");
            }
        }

        private bool PaymentPlanExists(int id)
        {
            return context.PaymentPlans.Any(e => e.ID == id);
        }
        private PaymentPlan ToPaymentPlan(PaymentPlanViewModel view)
        {
            return new PaymentPlan
            {
                ID = view.ID,
                Name = view.Name,
                Description = view.Description,
                Date = view.Date,
                Amount = view.Amount,
                ProjectID = view.ProjectID,
                SolicitanteID = view.SolicitanteID
            };
        }

        private PaymentPlanViewModel ToPaymentPlanViewModel(PaymentPlan paymentPlan)
        {
            var model = new PaymentPlanViewModel()
            {
                ID = paymentPlan.ID,
                Name = paymentPlan.Name,
                Description = paymentPlan.Description,
                Date = paymentPlan.Date,
                Amount = paymentPlan.Amount,

                SolicitanteID=paymentPlan.SolicitanteID,
                Users = combosHelper.GetComboUsers(),

                Project = paymentPlan.Project,
                ProjectName=paymentPlan.Project.Name,
                ProjectID = paymentPlan.ProjectID

            };

            return model;
        }

    }
}