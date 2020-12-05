using Pladeco.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pladeco.Domain
{
    public class Typology : IEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<TypologyStage> Stages { get; set; }

        public DateTime? create_date { get; set; }
        public int? create_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? write_uid { get; set; }
    }
}
