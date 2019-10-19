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
    public class PlanTask : IEntity
    {
        public PlanTask()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        public PlanTask(Plan plan)
        {
            this.Plan = plan;
            this.PlanID = plan.ID;
        }

        [Key]
        public int ID { get; set; }

        [DisplayName("Nombre")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Descripcion")]
        public string Description { get; set; }
        [DisplayName("Estado")]
        public eStatus Status { get; set; }
        [DisplayName("Fecha de inicio")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DisplayName("Fecha de fin")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [DisplayName("Prioridad")]
        public ePriority Priority { get; set; }

        [DisplayName("Responsable")]
        public string ResponsableID { get; set; }
        [ForeignKey("ResponsableID")]
        public User Responsable { get; set; }

        [DisplayName("Plan")]
        public int PlanID { get; set; }
        [ForeignKey("PlanID")]
        public virtual Plan Plan { get; set; }

        public DateTime? create_date { get; set; }
        public int? create_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? write_uid { get; set; }
    }
}
