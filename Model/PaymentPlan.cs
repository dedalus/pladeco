using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class PaymentPlan
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public User Solicitante { get; set; }
        public Project Project { get; set; }
    }
}
