using Pladeco.Model.Base;
using Pladeco.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pladeco.Model
{
    public class Plan : IEntity
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [DisplayName("Prioridad")]
        public ePriority Priority { get; set; }
        [DisplayName("Estado")]
        public eStatus Status { get; set; }
        [DisplayName("Fecha de inicio")]
        public DateTime StartDate { get; set; }
        [DisplayName("Fecha de fin")]
        public DateTime EndDate { get; set; }
        [DisplayName("Fecha de inicio real")]
        public DateTime RealStartDate { get; set; }
        [DisplayName("Fecha de fin real")]
        public DateTime RealEndDate { get; set; }

        [DisplayName("Proyecto")]
        public int ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }
        public User Responsable { get; set; }

        public ICollection<PlanTask> Tasks { get; set; }
        public DateTime? create_date { get; set; }
        public int? create_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? write_uid { get; set; }
    }
}
