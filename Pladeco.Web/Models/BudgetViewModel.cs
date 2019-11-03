using Microsoft.AspNetCore.Mvc.Rendering;
using Pladeco.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pladeco.Web.Models
{
    public class BudgetViewModel : Budget
    {
        [DisplayName("Area")]
        public string AreaName { get; set; }

        public IEnumerable<SelectListItem> Areas { get; set; }
    }
}
