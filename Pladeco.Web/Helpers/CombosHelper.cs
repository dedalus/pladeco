using Microsoft.AspNetCore.Mvc.Rendering;
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

        //public async Task<IEnumerable<SelectListItem>> GetComboUsers()
        //{
        //    var list = (await userHelper.GetAllUsersAsync()).Select(pt => new SelectListItem
        //    {
        //        Text = pt.Name,
        //        Value = $"{pt.Id}"
        //    })
        //        .OrderBy(pt => pt.Text)
        //        .ToList();

        //    //list.Insert(0, new SelectListItem
        //    //{
        //    //    Text = "[Select a pet type...]",
        //    //    Value = "0"
        //    //});

        //    return list;
        //}
    }
}
