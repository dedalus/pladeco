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
                PaymentPlan paymentPlan = this.ToPaymentPlan(view);

                paymentPlan.ID = 0;

                context.Add(paymentPlan);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { paymentPlan.ID });
            }


            return View(view);
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
                ProjectID = paymentPlan.ProjectID

            };

            return model;
        }

    }
}