using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Budget
    {
        [Key]
        public int ID { get; set; }
        public decimal Amount { get; set; }
        public Area Area { get; set; }
    }
}
