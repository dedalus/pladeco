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
    public class Project : IEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Descripción")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Prioridad")]
        public ePriority Priority { get; set; }

        [Required]
        [DisplayName("Area")]
        public int AreaID { get; set; }
        [ForeignKey("AreaID")]
        public virtual Area Area { get; set; }

        [DisplayName("Solicitante")]
        public string SolicitanteID { get; set; }
        [ForeignKey("SolicitanteID")]
        public User Solicitante { get; set; }

        [DisplayName("Responsable")]
        public string ResponsableID { get; set; }
        [ForeignKey("ResponsableID")]
        public User Responsable { get; set; }

        [DisplayName("Fecha de inicio")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DisplayName("Fecha de fin")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [DisplayName("Fecha de inicio real")]
        [DataType(DataType.Date)]
        public DateTime RealStartDate { get; set; }
        [DisplayName("Fecha de fin real")]
        [DataType(DataType.Date)]
        public DateTime RealEndDate { get; set; }
        public ICollection<Plan> Plans { get; set; }
        public ICollection<PaymentPlan> PaymentPlans { get; set; }
        public DateTime? create_date { get; set; }
        public int? create_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? write_uid { get; set; }
    }
}
