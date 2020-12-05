using Pladeco.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pladeco.Domain
{
    public class ProjectUser : IEntity
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public DateTime? create_date { get; set; }
        public int? create_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? write_uid { get; set; }
    }
}
