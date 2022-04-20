using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data_Transfer_Object;
using OnlineShop.Models;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        readonly IService<Category, CategoryDto>? _service;
        readonly ILogger<CategoryController> _logger;
        public CategoryController(IService<Category, CategoryDto> service, ILogger<CategoryController> logger)
        {
            this._service = service;
            this._logger = logger;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            IAsyncEnumerable<Category> categories; 
            try
            {
                categories = _service.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            return Ok(categories);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Category category;
            try
            {
                category = await _service.Get(id);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
            return Ok(category);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post(CategoryDto categoryDto)
        {
            await _service.Add(categoryDto);
            _logger.LogInformation("Added new Category");
            return Ok("Add Category");
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CategoryDto categoryDto)
        {
            try
            {
                await _service.Update(categoryDto, id);
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
