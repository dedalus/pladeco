using Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Plan
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public ePriority Priority { get; set; }
        public eStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RealStartDate { get; set; }
        public DateTime RealEndDate { get; set; }
        public Project Project { get; set; }
        public User Responsable { get; set; }

        public ICollection<Task> Task { get; set; }
    }
}
