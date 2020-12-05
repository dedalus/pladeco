using Microsoft.AspNetCore.Mvc.Rendering;
using Pladeco.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pladeco.Web.Models
{
    public class PaymentPlanViewModel : PaymentPlan
    {
        public string ProjectName { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
