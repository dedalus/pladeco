using Pladeco.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pladeco.Model
{
    public class Budget : IEntity
    {
        public Budget()
        {
            Area = null;
        }

        [Key]
        public int ID { get; set; }
        [DisplayName("Monto")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        [Required]
        [DisplayName("Area")]
        public int AreaID { get; set; }
        [ForeignKey("AreaID")]
        [DisplayName("Area")]
        public virtual Area Area { get; set; }
        public DateTime? create_date { get; set; }
        public int? create_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? write_uid { get; set; }
    }
}
