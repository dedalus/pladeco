using System;
using System.Collections.Generic;
using System.Text;

namespace Pladeco.Model.Base
{
    public interface IEntity
    {
        int ID { get; set; }
        DateTime? create_date { get; set; }
        int? create_uid { get; set; }
        DateTime? write_date { get; set; }
        int? write_uid { get; set; }
    }
}
