using EnocaApiChallenge.Models;
using EnocaApiChallenge.Utility;
using Microsoft.AspNetCore.Mvc;

namespace EnocaApiChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public OrdersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders.ToList();
            return Ok(orders);
        }

        // POST: api/Orders
        [HttpPost]
        public IActionResult PostOrder(Order order)
        {
            var orderDesi = order.OrderDesi;
            var carrierConfigurations = _context.CarrierConfigurations.ToList();

            var matchingCarrier = carrierConfigurations.FirstOrDefault(c => orderDesi >= c.CarrierMinDesi && orderDesi <= c.CarrierMaxDesi);
            if (matchingCarrier != null)
            {
                order.OrderCarrierCost = matchingCarrier.CarrierCost;
            }
            else
            {
                var nearestCarrier = carrierConfigurations.OrderBy(c => Math.Abs(orderDesi - c.CarrierMinDesi)).First();
                var difference = orderDesi - nearestCarrier.CarrierMinDesi;
                order.OrderCarrierCost = nearestCarrier.CarrierCost + (nearestCarrier.CarrierCost * difference);
            }

            _context.Orders.Add(order);
            _context.SaveChanges();

            return CreatedAtAction("GetOrders", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
