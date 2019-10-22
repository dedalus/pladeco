using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pladeco.Model.Enum
{
    public enum eStatus
    {
        [Display(Name = "No iniciado")]
        PENDING = 0,
        [Display(Name = "En proceso")]
        IN_PROCESS = 1,
        [Display(Name = "Finalizado")]
        DONE = 2
    }
}
