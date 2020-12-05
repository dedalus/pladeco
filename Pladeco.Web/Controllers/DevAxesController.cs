using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pladeco.Domain;
using Pladeco.Web.Data;

namespace Pladeco.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DevAxesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevAxesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET:.DevAxes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DevAxes.ToListAsync());
        }

        // GET:.DevAxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dexAxis = await _context.DevAxes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dexAxis == null)
            {
                return NotFound();
            }

            return View(dexAxis);
        }

        // GET:.DevAxes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketChannels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ID")] DevAxis devAxis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(devAxis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(devAxis);
        }

        // GET:.DevAxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devAxis = await _context.DevAxes.FindAsync(id);
            if (devAxis == null)
            {
                return NotFound();
            }
            return View(devAxis);
        }

        // POST:.DevAxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ID")] DevAxis devAxis)
        {
            if (id != devAxis.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(devAxis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DevAxisExists(devAxis.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(devAxis);
        }

        // GET:.DevAxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketChannel = await _context.DevAxes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ticketChannel == null)
            {
                return NotFound();
            }

            return View(ticketChannel);
        }

        // POST: TicketChannels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var devAxis = await _context.DevAxes.FindAsync(id);
            _context.DevAxes.Remove(devAxis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DevAxisExists(int id)
        {
            return _context.DevAxes.Any(e => e.ID == id);
        }
    }
}
