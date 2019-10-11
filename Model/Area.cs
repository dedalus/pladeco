using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Area
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
