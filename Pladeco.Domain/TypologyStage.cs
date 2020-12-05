using Pladeco.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pladeco.Domain
{
    public class TypologyStage : IEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        [DisplayName("Tipología")]
        public int TypologyID { get; set; }
        [ForeignKey("TypologyID")]
        public virtual Typology Typology { get; set; }

        public DateTime? create_date { get; set; }
        public int? create_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? write_uid { get; set; }
    }
}
