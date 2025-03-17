using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Model;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                return await _context.Product.ToListAsync();


            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = "Se presento un inconveniente inesperado.", Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _context.Product.FindAsync(id);

                if (product == null)
                {
                    return NotFound(new { Message = $"No se encontro un producto con el id {id}." });
                }

                return product;
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = $"Se presento un error inesperado al intentar consultar el producto con id {id}", Error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            try
            {
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProducts), new { product.id }, product);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = "Se presento un error inesperado al intentar crear el producto", Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product updatedProduct)
        {
            try
            {
                var product = await _context.Product.FindAsync(id);

                if (product == null)
                {
                    return NotFound(new { Message = $"No se encontro el producto con id {id} en la base de datos." });
                }

                product.NameProduct = updatedProduct.NameProduct;
                product.Description = updatedProduct.Description;
                product.Price = updatedProduct.Price;

                _context.Entry(product).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok(new { Message = $"El producto con el id {id} fue editado correctamente", product });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = $"Se presento un error inesperado al intentar eliminar el producto con id {id}.", Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Product.FindAsync(id);

                if (product == null)
                {
                    return NotFound(new { Message = $"No se encontro un producto con el id {id} en la base de datos." });
                }

                _context.Product.Remove(product);
                await _context.SaveChangesAsync();

                return Ok(new { Message = $"El producto con el id {id} fue eliminado correctamente" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = $"Se presento un error inesperado al intentar eliminar el producto con id {id}", Error = ex.Message });
            }
        }
    }
}
