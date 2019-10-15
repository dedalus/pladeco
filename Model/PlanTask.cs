using Pladeco.Model.Base;
using Pladeco.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pladeco.Model
{
    public class PlanTask : IEntity
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
        public DateTime? create_date { get; set; }
        public int? create_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? write_uid { get; set; }
    }
}
