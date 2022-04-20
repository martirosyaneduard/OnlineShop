using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data_Transfer_Object;
using OnlineShop.Models;
using OnlineShop.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly IService<Order, OrderDto>? _service;
        readonly ILogger<OrderController> _logger;
        public OrderController(IService<Order, OrderDto> service, ILogger<OrderController> logger)
        {
            this._service = service;
            this._logger = logger;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = _service.GetAll();
            return Ok(orders);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Order order;
            try
            {
                order = await _service.Get(id);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
            return Ok(order);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post(OrderDto orderDto)
        {
            await _service.Add(orderDto);
            _logger.LogInformation("Added new Order");
            return Ok("Order Added");
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, OrderDto orderDto)
        {
            try
            {
                await _service.Update(orderDto, id);
                _logger.LogInformation("Updated, {id}", id);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                _logger.LogInformation("Deleted, {id}", id);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                NotFound(ex.Message);
                _logger.LogError(ex.Message);
            }

            return Ok();
        }
    }
}
