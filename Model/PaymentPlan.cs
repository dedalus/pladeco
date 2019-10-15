using Pladeco.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pladeco.Model
{
    public class PaymentPlan: IEntity
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [DisplayName("Monto")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public User Solicitante { get; set; }
        public Project Project { get; set; }
        public DateTime? create_date { get; set; }
        public int? create_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? write_uid { get; set; }
    }
}
