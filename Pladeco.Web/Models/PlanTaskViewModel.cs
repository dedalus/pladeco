using Microsoft.AspNetCore.Mvc.Rendering;
using Pladeco.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pladeco.Web.Models
{
    public class PlanTaskViewModel : PlanTask
    {
        public PlanTaskViewModel()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        public SelectList Users { get; set; }
        public SelectList Priorities { get; set; }
    }
}
