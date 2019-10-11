using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class User : IdentityUser
    {
        public User() : base() { }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [MaxLength(200)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

    }
}
