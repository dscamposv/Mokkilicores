using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mokkilicores.Models;
using Microsoft.Extensions.Caching.Memory;
using Mokkilicores.Data;

namespace Mokkilicores.Controllers
{
    public class ArticuloController : Controller
    {

        private readonly IMemoryCache _cache;

        public ArticuloController(IMemoryCache cache)
        {
            _cache = cache;
        }

        // GET: ArticuloController
        public ActionResult NuevoArticulo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevoArticulo(Articulo articulo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Conexion datos = new Conexion(_cache);
                    List<Articulo> tempList = datos.GetArticulo();
                    var existe = tempList.Any(c => c.Id == articulo.Id);
                    if (existe)
                    {
                        ModelState.AddModelError("Id", "El ID ya existe.");
                        return View(articulo);
                    }
                    datos.addArticulo(articulo);
                    ViewBag.MensajeExito = "El artículo ha sido guardado exitosamente ";
                    ModelState.Clear();
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ListaArticulo()
        {
            List<Articulo> tempList = new List<Articulo>();
            Conexion datos = new Conexion(_cache);
            tempList = datos.GetArticulo();

            if (tempList.Count > 0 || tempList != null)
            {

                return View(tempList);

            }
            else { return View(); }

            
        }

        [HttpGet]
        public ActionResult EditaArticulo(string id)
        {
            List<Articulo> tempList = new List<Articulo>();
            Conexion datos = new Conexion(_cache);
            tempList = datos.GetArticulo();

            var edit = tempList.FirstOrDefault(c => c.Id == id);

            return View(edit);
        }

        [HttpPost]
        public ActionResult EditaArticulo(Articulo articulo)
        {
            try
            {
                    Conexion datos = new Conexion(_cache); 
                    datos.editArticulo(articulo);
                    ViewBag.MensajeExito = "El artículo ha sido actualizado exitosamente ";
                    ModelState.Clear();
                
                return RedirectToAction("ListaArticulo");
            }
            catch
            {
                return RedirectToAction("ListaArticulo");
            }
        }

        [HttpGet]
        public ActionResult EliminaArticulo(string id)
        {
            try
            {
                Conexion datos = new Conexion(_cache);
                List<Articulo> tempList = new List<Articulo>();
                tempList = datos.GetArticulo();

                var articulo = tempList.FirstOrDefault(c => c.Id == id);

                if ( articulo!= null) { 
                    datos.eliminaArticulo(articulo);
                    TempData["mensaje"] = "Eliminado correctamente";
                    return RedirectToAction("ListaArticulo");
                }


                TempData["mensaje"] = "No se ha podido eliminar";
                return RedirectToAction("ListaArticulo");
            }
            catch
            {
                return RedirectToAction("ListaArticulo");
            }
        }










    }
}
