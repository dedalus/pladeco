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

        
        [DisplayName("Fuente de financiamiento")]
        [Required(ErrorMessage = "Debes seleccionar una fuente de financiamiento")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una fuente de financiamiento")]
        public int AreaID { get; set; }
        [ForeignKey("AreaID")]
        public virtual Area Area { get; set; }

        [DisplayName("Solicitante")]
        [Required(ErrorMessage = "Debes seleccionar un solicitante")]
        [MinLength(1)]
        public string SolicitanteID { get; set; }
        [ForeignKey("SolicitanteID")]
        public User Solicitante { get; set; }

        [DisplayName("Responsable")]
        [Required(ErrorMessage = "Debes seleccionar un responsable")]
        [MinLength(1)]
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

        [DisplayName("Sector")]
        [Required(ErrorMessage = "Debes seleccionar un sector")]
        public int SectorID { get; set; }
        [ForeignKey("SectorID")]
        public Sector Sector { get; set; }


        [DisplayName("Unidad responsable")]
        [Required(ErrorMessage = "Debes seleccionar una unidad responsable")]
        public int ResponsableUnitID { get; set; }
        [DisplayName("Unidad responsable")]
        [ForeignKey("ResponsableUnitID")]
        public ResponsableUnit ResponsableUnit { get; set; }

        [DisplayName("Eje de desarrollo")]
        [Required(ErrorMessage = "Debes seleccionar un eje de desarrollo")]
        public int DevAxisID { get; set; }
        [ForeignKey("DevAxisID")]
        public DevAxis DevAxis { get; set; }

        [DisplayName("Tipología")]
        [Required(ErrorMessage = "Debes seleccionar una Tipología")]
        public int TypologyID { get; set; }
        [DisplayName("Tipología")]
        [ForeignKey("TypologyID")]
        public Typology Typology { get; set; }

        [DisplayName("Etapa")]
        [Required(ErrorMessage = "Debes seleccionar una etapa de la tipología")]
        public int StageID { get; set; }
        [DisplayName("Etapa")]
        [ForeignKey("StageID")]
        public TypologyStage Stage { get; set; }

        [DisplayName("Descripción")]
        public string BudgetDescription { get; set; }

        [DisplayName("Presupuesto")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? BudgetAmount { get; set; }

        [DisplayName("Responsable de Plan de Gasto")]
        public string ResponsableBudgetID { get; set; }
        [ForeignKey("ResponsableBudgetID")]
        public User ResponsableBudget { get; set; }


        [DisplayName("Objectivo estratégico")]
        public string StrategyTarget { get; set; }

        [DisplayName("Medio de verificación")]
        public string VerificationUnit { get; set; }

        [DisplayName("Indicador de cumpliento")]
        public string ComplianceIndicator { get; set; }

        [NotMapped]
        public int Porc { get; set; }

        [NotMapped]
        public int DoneTasks { get; set; }
        [NotMapped]
        public int TotalTasks { get; set; }

        public ICollection<Plan> Plans { get; set; }
        public List<ProjectUser> Colaborators { get; set; }
        public ICollection<PaymentPlan> PaymentPlans { get; set; }
        public DateTime? create_date { get; set; }
        public int? create_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? write_uid { get; set; }

        public Project()
        {
            Colaborators = new List<ProjectUser>();

            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            RealStartDate = DateTime.Now;
            RealEndDate = DateTime.Now;
        }
    }
}
