using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pladeco.Web.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly IJsReportMVCService jsReportMVCService;

        public ReportsController(IJsReportMVCService jsReportMVCService)
        {
            this.jsReportMVCService = jsReportMVCService;
        }

        [MiddlewareFilter(typeof(JsReportPipeline))]
        public IActionResult Projects()
        {
            HttpContext.JsReportFeature().Recipe(Recipe.ChromePdf);

            return View();
        }
    }
}