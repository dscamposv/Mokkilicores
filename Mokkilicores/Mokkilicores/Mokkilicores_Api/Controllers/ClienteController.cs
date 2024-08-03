using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Mokkilicores.Models;
using Mokkilicores_api.Data;

namespace Mokkilicores_Api.Controllers
{
    [Route("api/Cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMemoryCache ElCache;

        public ClienteController (IMemoryCache cache)
        {
            ElCache = cache;
        }


        // GET: api/Cliente
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetCliente()
        {
            Memoria datos = new Memoria(ElCache);
            List<Cliente> laLista = datos.ObtengaLaListaDeClientes();
            return Ok(laLista);
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public ActionResult<Articulo> GetCliente(string id)
        {
            Memoria datos = new Memoria(ElCache);
            Cliente cliente = datos.ObtengaCliente(id);
            if (cliente is null)
                return NotFound();
            else
                return Ok(cliente);
        }

        // POST: api/Cliente
        [HttpPost]
        public ActionResult Post(Cliente value)
        {
            Memoria datos = new Memoria(ElCache);
            datos.AgregarCliente(value);
            return Ok(value);
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, Cliente value)
        {
            Memoria datos = new Memoria(ElCache);
            Cliente cliente = datos.ObtengaCliente(id);
            if (cliente is null)
                return NotFound();
            else
            {
                datos.EditarCliente(value);
                return Ok(value);
            }
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            Memoria datos = new Memoria(ElCache);
            Cliente cliente = datos.ObtengaCliente(id);
            if (cliente is null)
                return NotFound();
            else
            {
                datos.EliminarCliente(cliente.Id);
                return Ok(cliente);
            }
        }
    }
}
