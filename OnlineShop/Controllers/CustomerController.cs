using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data_Transfer_Object;
using OnlineShop.Models;
using OnlineShop.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly ICustomerQuery _customerQuery;
        readonly IService<Customer, CustomerDto>? _service;
        readonly ILogger<CustomerController> _logger;
        public CustomerController(IService<Customer, CustomerDto> service, ICustomerQuery customerQuery, ILogger<CustomerController> logger)
        {
            this._service = service;
            this._customerQuery = customerQuery;
            this._logger = logger;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = _service.GetAll();
            return Ok(customers);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Customer customer;
            try
            {
                customer = await _service.Get(id);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
            return Ok(customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post(CustomerDto customerDto)
        {
            await _service.Add(customerDto);
            _logger.LogInformation("Added new Customer");
            return Ok("Customer Added");
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CustomerDto customerDto)
        {
            try
            {
                await _service.Update(customerDto, id);
                _logger.LogInformation("Updated, {id}", id);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            catch(Exception ex)
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
                _logger.LogInformation("Deleted, {id}",id);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
            catch(Exception ex)
            {
                NotFound(ex.Message);
                _logger.LogError(ex,"");
            }

            return Ok();
        }

        [HttpGet("Name")]
        public async Task<IActionResult> GetCustomerName()
        {
            var name = await _customerQuery.GetMostOrdersName();
            return Ok(name);
        }
    }
}
