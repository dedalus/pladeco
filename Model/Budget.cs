using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pladeco.Model
{
    public class Budget
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
    }
}
