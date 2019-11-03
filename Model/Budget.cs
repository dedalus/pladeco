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
        [Key]
        public int ID { get; set; }

        [DisplayName("Año")]
        public int Year { get; set; }
        [DisplayName("Area")]
        [Required(ErrorMessage = "Debes seleccionar un Area")]
        public int AreaID { get; set; }
        [ForeignKey("AreaID")]
        public Area Area { get; set; }

        [DisplayName("Monto")]
        [Required(ErrorMessage = "Debes seleccionar un Area")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        public DateTime? create_date { get; set; }
        public int? create_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? write_uid { get; set; }
    }
}
