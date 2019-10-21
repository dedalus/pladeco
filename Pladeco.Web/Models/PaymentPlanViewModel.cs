using Microsoft.AspNetCore.Mvc.Rendering;
using Pladeco.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pladeco.Web.Models
{
    public class PaymentPlanViewModel : PaymentPlan
    {
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
