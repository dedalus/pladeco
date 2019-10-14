using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pladeco.Model
{
    public class PaymentPlan
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
    }
}
