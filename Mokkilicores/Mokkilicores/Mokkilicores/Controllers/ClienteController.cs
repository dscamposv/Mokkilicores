using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mokkilicores.Models;
using Microsoft.Extensions.Caching.Memory;
using Mokkilicores.Data;

namespace Mokkilicores.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IMemoryCache _cache;

        public ClienteController(IMemoryCache cache)
        {
            _cache = cache;
        }

        // GET: ClienteController
        public ActionResult NuevoCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevoCliente(Cliente cliente)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Conexion datos = new Conexion(_cache);
                    List<Cliente> tempList = datos.GetCliente();
                    var existe = tempList.Any(c => c.Id == cliente.Id);

                    if (existe)
                    {
                        ModelState.AddModelError("Id", "El ID ya existe.");
                        return View(cliente);
                    }

                    var idCliente = cliente.Id;
                    var nombreCliente = cliente.Nombre;
                    var apellidoCliente = cliente.Apellidos;
                    var apellidoMayus = apellidoCliente.ToUpper();
                    var passCliente = idCliente + nombreCliente.Substring(0, 2) + apellidoMayus[0];

                    cliente.CompraAnual = 0;
                    cliente.CompraSemestral = 0;
                    cliente.CompraTotal = 0;
                    cliente.PassWord = passCliente;

                    datos.addCliente(cliente);

                    ViewBag.MensajeExito = "El cliente " + nombreCliente + " " + apellidoCliente + " tendrá los siguientes datos para inicio de sesión \\n ID: " + idCliente + " \\n Contraseña: " + passCliente + "\\nSus datos serán necesarios para realizar una orden";

                    ModelState.Clear();
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ListaCliente()
        {
            List<Cliente> tempList = new List<Cliente>();
            Conexion datos = new Conexion(_cache);
            tempList = datos.GetCliente();

            if (tempList.Count > 0 || tempList != null)
            {

                return View(tempList);

            }
            else { return View(); }
        }


        [HttpGet]
        public ActionResult EditaCliente(string id)
        {
            List<Cliente> tempList = new List<Cliente>();
            Conexion datos = new Conexion(_cache);
            tempList = datos.GetCliente();

            var edit = tempList.FirstOrDefault(c => c.Id == id);

            return View(edit);
        }

        [HttpPost]
        public ActionResult EditaCliente(Cliente cliente)
        {
            try
            {
                Conexion datos = new Conexion(_cache);
                datos.editCliente(cliente);
                ViewBag.MensajeExito = "El cliente ha sido actualizado exitosamente ";
                ModelState.Clear();

                return RedirectToAction("ListaCliente");
            }
            catch
            {
                return RedirectToAction("ListaCliente");
            }
        }


        [HttpGet]
        public ActionResult EliminaCliente(string id)
        {
            try
            {
                Conexion datos = new Conexion(_cache);
                List<Cliente> tempList = new List<Cliente>();
                tempList = datos.GetCliente();

                var cliente = tempList.FirstOrDefault(c => c.Id == id);

                if (cliente != null)
                {
                    datos.eliminaCliente(cliente);
                    TempData["mensaje"] = "Eliminado correctamente";
                    return RedirectToAction("ListaCliente");
                }


                TempData["mensaje"] = "No se ha podido eliminar";
                return RedirectToAction("ListaCliente");
            }
            catch
            {
                return RedirectToAction("ListaArticulo");
            }
        }

















        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
