using Pladeco.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pladeco.Model
{
    public class PlanTask
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Descripcion")]
        public string Description { get; set; }
        [DisplayName("Estado")]
        public eStatus Status { get; set; }
        [DisplayName("Fecha de inicio")]
        public DateTime StartDate { get; set; }
        [DisplayName("Fecha de fin")]
        public DateTime EndDate { get; set; }

    }
}
