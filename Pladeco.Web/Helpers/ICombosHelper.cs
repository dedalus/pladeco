using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pladeco.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboAreas();
        IEnumerable<SelectListItem> GetComboUsers();
        IEnumerable<SelectListItem> GetComboRoles();
        IEnumerable<SelectListItem> GetComboSectors();
        IEnumerable<SelectListItem> GetComboResponsableUnits();
        IEnumerable<SelectListItem> GetComboDevAxes();
        IEnumerable<SelectListItem> GetComboTypologies();
        IEnumerable<SelectListItem> GetComboStages(int typologyID);
    }
}
