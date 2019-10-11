using Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Project
    {
        [Key]
        public int ID { get; set; }
        public ePriority Priority { get; set; }

        public Area Area { get; set; }
        public User Solicitante { get; set; }
        public User Responsable { get; set; }
        public ICollection<Plan> Plans { get; set; }
        public ICollection<PaymentPlan> PaymentPlans { get; set; }
    }
}
