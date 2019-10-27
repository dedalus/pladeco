using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pladeco.Model
{
    public class User : IdentityUser
    {
        public User() : base() 
        {
            Active = true;
        }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [MaxLength(200)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [DisplayName("Area")]
        public int? AreaID { get; set; }
        [ForeignKey("AreaID")]
        public virtual Area Area { get; set; }

        public ICollection<ProjectUser> Projects { get; set; }

        public bool Active { get; set; }

    }
}
