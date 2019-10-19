using Pladeco.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pladeco.Web.Models
{
    public class BudgetViewModel
    {
        [DisplayName("Area")]
        public int AreaID { get; set; }
        [DisplayName("Area")]
        public string AreaName { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DisplayName("Monto")]
        public decimal Amount { get; set; }
    }
}
