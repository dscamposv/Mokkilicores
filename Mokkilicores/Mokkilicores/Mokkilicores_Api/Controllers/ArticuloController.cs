using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Mokkilicores.Models;
using Mokkilicores.Data;
using Mokkilicores_api.Data;


namespace Mokkilicores_api .Controllers
{
    [Route("api/Articulo")]
    [ApiController]
    public class ArticuloController : ControllerBase

    {
        private readonly IMemoryCache ElCache;

        public ArticuloController(IMemoryCache cache)
        {
            ElCache = cache;
        }
        // GET: api/Articulo
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetArticulo()
        {
            Memoria datos = new Memoria(ElCache);
            List<Articulo> laLista = datos.ObtengaLaListaDeArticulos();
            return Ok(laLista);
        }

        // GET: api/Articulo/5
        [HttpGet("{id}")]
        public ActionResult<Articulo> GetArticulo(int id)
        {
            Memoria datos = new Memoria(ElCache);
            Articulo articulo = datos.ObtengaArticulo(id);
            if (articulo is null)
                return NotFound();
            else
                return Ok(articulo);
        }

        // POST: api/Articulo
        [HttpPost]
        public ActionResult Post(Articulo value)
        {
            Memoria datos = new Memoria(ElCache);
            datos.AgregarArticulo(value);
            return Ok(value);
        }

        // PUT: api/Articulo/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Articulo value)
        {
            Memoria datos = new Memoria(ElCache);
            Articulo articulo = datos.ObtengaArticulo(id);
            if (articulo is null)
                return NotFound();
            else
            {
                datos.EditarArticulo(value);
                return Ok(value);
            }
        }

        // DELETE: api/Articulo/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Memoria datos = new Memoria(ElCache);
            Articulo articulo = datos.ObtengaArticulo(id);
            if (articulo is null)
                return NotFound();
            else
            {
                datos.EliminarArticulo(articulo.Id);
                return Ok(articulo);
            }
        }

    }
}
