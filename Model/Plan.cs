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

        public Plan(Project project)
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            this.Project = project;
            this.ProjectID = project.ID;
        }

        public Plan()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }
        [DisplayName("Nombre")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [DisplayName("Prioridad")]
        public ePriority Priority { get; set; }
        [DisplayName("Estado")]
        public eStatus Status { get; set; }
        [DisplayName("Fecha de inicio")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DisplayName("Fecha de fin")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [DisplayName("Fecha de inicio real")]
        public DateTime RealStartDate { get; set; }
        [DisplayName("Fecha de fin real")]
        public DateTime RealEndDate { get; set; }

        [DisplayName("Responsable")]
        [Required(ErrorMessage = "Debes seleccionar un responsable")]
        public string ResponsableID { get; set; }
        [ForeignKey("ResponsableID")]
        public virtual User Responsable { get; set; }

        [DisplayName("Proyecto")]
        public int ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }

        [NotMapped]
        public int Porc { get; set; }

        public ICollection<PlanTask> Tasks { get; set; }
        public DateTime? create_date { get; set; }
        public int? create_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? write_uid { get; set; }
    }
}
