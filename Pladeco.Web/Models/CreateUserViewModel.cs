﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pladeco.Web.Models
{
    public class CreateUserViewModel
    {
        public string ID { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Confirm { get; set; }

        [DisplayName("Area")]
        public int? AreaID { get; set; }

        [DisplayName("Rol")]
        [Required(ErrorMessage ="Debe seleccionar un rol")]
        public string RoleID { get; set; }

        [DisplayName("Activo")]
        public bool Active { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

        public SelectList Areas { get; set; }
    }
}
