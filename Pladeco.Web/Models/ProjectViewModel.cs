using Microsoft.AspNetCore.Mvc.Rendering;
using Pladeco.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Colaboradores")]
        public IEnumerable<string> SelectedUsers { get; set; }
        public IEnumerable<SelectListItem> ColaboratorsList { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<SelectListItem> Areas { get; set; }
        public SelectList Priorities { get; set; }
        public IEnumerable<SelectListItem> ResponsableUnits { get; set; }
        public IEnumerable<SelectListItem> Sectors { get; set; }
        public IEnumerable<SelectListItem> DevAxes { get; set; }
        public IEnumerable<SelectListItem> Typologies { get; set; }
        public IEnumerable<SelectListItem> Stages { get; set; }
    }
}
