using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pladeco.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pladeco.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly ApplicationDbContext context;
        private readonly IUserHelper userHelper;

        public CombosHelper(ApplicationDbContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
        }

        public IEnumerable<SelectListItem> GetComboAreas()
        {
            var list = context.Areas.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.ID}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "",
                Value = ""
            });

            return list;
        }


        public IEnumerable<SelectListItem> GetComboSectors()
        {
            var list = context.Sectors.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.ID}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "",
                Value = ""
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboResponsableUnits()
        {
            var list = context.ResponsableUnits.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.ID}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "",
                Value = ""
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboDevAxes()
        {
            var list = context.DevAxes.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.ID}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "",
                Value = ""
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboTypologies()
        {
            var list = context.Typologies.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.ID}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "",
                Value = ""
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboStages(int typologyID)
        {
            var typology = this.context.Typologies.Find(typologyID);
            var list = new List<SelectListItem>();
            if (typology != null)
            {
                list = typology.Stages.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.ID.ToString()
                }).OrderBy(l => l.Text).ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "",
                Value = ""
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboUsers()
        {
            var list = context.Users.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.Id}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            //list.Insert(0, new SelectListItem
            //{
            //    Text = "",
            //    Value = ""
            //});

            return list;
        }

        public IEnumerable<SelectListItem> GetComboRoles()
        {
            var list = context.Roles.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.Id}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "",
                Value = ""
            });

            return list;
        }

    }
}
