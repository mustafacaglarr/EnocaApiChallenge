using EnocaApiChallenge.Models;
using EnocaApiChallenge.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnocaApiChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierConfigurationsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CarrierConfigurationsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/CarrierConfigurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarrierConfiguration>>> GetCarrierConfigurations()
        {
            return await _context.CarrierConfigurations.ToListAsync();
        }

        // POST: api/CarrierConfigurations
        [HttpPost]
        public async Task<ActionResult<CarrierConfiguration>> PostCarrierConfiguration(CarrierConfiguration carrierConfiguration)
        {
            _context.CarrierConfigurations.Add(carrierConfiguration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrierConfigurations", new { id = carrierConfiguration.CarrierConfigurationId }, carrierConfiguration);
        }

        // PUT: api/CarrierConfigurations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrierConfiguration(int id, CarrierConfiguration carrierConfiguration)
        {
            if (id != carrierConfiguration.CarrierConfigurationId)
            {
                return BadRequest();
            }

            _context.Entry(carrierConfiguration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrierConfigurationExists(id))
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

        // DELETE: api/CarrierConfigurations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrierConfiguration(int id)
        {
            var carrierConfiguration = await _context.CarrierConfigurations.FindAsync(id);
            if (carrierConfiguration == null)
            {
                return NotFound();
            }

            _context.CarrierConfigurations.Remove(carrierConfiguration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarrierConfigurationExists(int id)
        {
            return _context.CarrierConfigurations.Any(e => e.CarrierConfigurationId == id);
        }
    }
}
