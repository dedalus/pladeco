using Pladeco.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pladeco.Domain
{
    public class PaymentPlan: IEntity
    {
        public PaymentPlan()
        {
            Date = DateTime.Now;
        }
        public PaymentPlan(Project project)
        {
            Date = DateTime.Now;

            this.Project = project;
            this.ProjectID = project.ID;
        }

        [Key]
        public int ID { get; set; }
        [DisplayName("Nombre")]
        public string Name { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [DisplayName("Monto")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        [DisplayName("Solicitante")]
        public string SolicitanteID { get; set; }
        [ForeignKey("SolicitanteID")]
        public virtual User Solicitante { get; set; }

        [DisplayName("Proyecto")]
        public int ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }


        [DisplayName("Fecha")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public DateTime? create_date { get; set; }
        public int? create_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? write_uid { get; set; }
    }
}
