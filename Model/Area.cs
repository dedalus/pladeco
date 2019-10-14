using Pladeco.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pladeco.Model
{
    public class Area : IEntity
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Nombre")]
        public string Name { get; set; }
        [DisplayName("Habilitada")]
        public bool Active { get; set; }
        public DateTime? create_date { get; set; }
        public int? create_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? write_uid { get; set; }
    }
}
