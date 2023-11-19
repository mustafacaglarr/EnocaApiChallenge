using EnocaApiChallenge.Models;
using EnocaApiChallenge.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnocaApiChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarriersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CarriersController(MyDbContext context)
        {
            _context = context;
        }

        // Tüm kargo firmalarını listeleme
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrier>>> GetCarriers()
        {
            return await _context.Carriers.ToListAsync();
        }

        // Kargo firması ekleme
        [HttpPost]
        public async Task<ActionResult<Carrier>> PostCarrier(Carrier carrier)
        {
            _context.Carriers.Add(carrier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarriers", new { id = carrier.CarrierId }, carrier);
        }

        // Kargo firması güncelleme
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrier(int id, Carrier carrier)
        {
            if (id != carrier.CarrierId)
            {
                return BadRequest();
            }

            _context.Entry(carrier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrierExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Kargo firması silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrier(int id)
        {
            var carrier = await _context.Carriers.FindAsync(id);
            if (carrier == null)
            {
                return NotFound();
            }

            _context.Carriers.Remove(carrier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarrierExists(int id)
        {
            return _context.Carriers.Any(e => e.CarrierId == id);
        }
    }
}
