using Microsoft.AspNetCore.Mvc.Rendering;
using Pladeco.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pladeco.Web.Models
{
    public class ProjectViewModel : Project
    {
        public ProjectViewModel()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            RealStartDate = DateTime.Now;
            RealEndDate = DateTime.Now;
        }
        public SelectList Users { get; set; }
        public SelectList Solicitantes { get; set; }
        public SelectList Areas { get; set; }
        public SelectList Priorities { get; set; }
    }
}
