using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data_Transfer_Object;
using OnlineShop.Models;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IService<Product, ProductDto>? _service;
        readonly ILogger<ProductController> _logger;
        public ProductController(IService<Product, ProductDto> service, ILogger<ProductController> logger)
        {
            this._service = service;
            this._logger = logger;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = _service.GetAll();
            return Ok(products);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Product product;
            try
            {
                product = await _service.Get(id);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
            return Ok(product);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post(ProductDto productDto)
        {
            await _service.Add(productDto);
            _logger.LogInformation("Added new Product");
            return Ok("Product Added");
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductDto productDto)
        {
            try
            {
                await _service.Update(productDto, id);
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
