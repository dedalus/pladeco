using Microsoft.AspNetCore.Mvc.Rendering;
using Pladeco.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pladeco.Web.Models
{
    public class ProjectBudgetViewModel
    {
        public int AreaID { get; set; }
        [DisplayName("Proyecto")]
        public int ProjectID { get; set; }
        [DisplayName("Area")]
        public string ProjectName { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DisplayName("Monto")]
        public decimal Amount { get; set; }
        [DisplayName("Descripción")]
        public string BudgetDescription { get; set; }

        [DisplayName("Responsable de Plan de Gasto")]
        public string ResponsableBudgetID { get; set; }
        public User ResponsableBudget { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
