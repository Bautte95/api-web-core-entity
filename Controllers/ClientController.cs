using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Model;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase 
    {
        private readonly AppDbContext _context;

        public ClientController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            try
            {
                return await _context.Client.ToListAsync();
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = "Se presento un error inesperado al consultar los clientes.", Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            try
            {
                var client = await _context.Client.FindAsync(id);

                if (client == null)
                {
                    return NotFound(new { Message = $"El cliente con el id {id} no se encontro en la base de datos." });
                }

                return client;
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = $"Se presento un error inesperado al intentar consultar el cliente con id {id}.", Error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(Client client)
        {
            try
            {
                await _context.Client.AddAsync(client);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetClients), new { client.Id }, client);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = "Se presento un error inesperado al intentar crear al cliente", Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, Client updatedClient)
        {
            try
            {
                var client = await _context.Client.FindAsync(id);

                if (client == null)
                {
                   return NotFound(new { Message = $"No se encontro un cliente con id {id} en la base de datos." });
                }

                client.Email = updatedClient.Email;
                client.Cellular = updatedClient.Cellular;
                client.Name = updatedClient.Name;

                _context.Entry(client).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new { Message = $"El cliente con id {id} fue modificado correctamente", client });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = $"Se presento un error inesperado al intentar editar al cliente con id {id}.", Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var client = await _context.Client.FindAsync(id);

                if (client == null)
                {
                    return NotFound(new { Message = $"No se encontro un cliente con el id {id} en la base de datos." });
                }

                _context.Client.Remove(client);
                await _context.SaveChangesAsync();

                return Ok(new { Message = $"El cliente con el id {id} fue eliminado correctamente" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = $"Se presento un error inesperado al intentar eliminar el cliente con el id {id}.", Error = ex.Message });
            }
        }
    }
}
