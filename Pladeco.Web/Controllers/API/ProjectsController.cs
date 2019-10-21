using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pladeco.Web.Data;

namespace Pladeco.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProjectsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> GetStagesAsync(int typologyID)
        {
            var typology = await context.Typologies.Include(t => t.Stages).Where(t => t.ID == typologyID).FirstOrDefaultAsync();
            return Ok(typology.Stages.OrderBy(c => c.Name).ToList());
        }
    }
}