using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pladeco.Model.Enum
{
    public enum ePriority
    {
        [Display(Name = "Baja")]
        Low =0,
        [Display(Name = "Normal")]
        Normal =1,
        [Display(Name = "Alta")]
        High =2,
        [Display(Name = "Urgente")]
        Urgent =3
    }
}
