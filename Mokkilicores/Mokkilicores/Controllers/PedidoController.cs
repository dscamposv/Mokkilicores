using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Mokkilicores.Data;
using Mokkilicores.Models;

namespace Mokkilicores.Controllers
{
    //[Authorize]
    public class PedidoController : Controller
    {
        private readonly IMemoryCache _cache;

        public PedidoController(IMemoryCache cache)
        {
            _cache = cache;
        }


        [HttpGet]
        public ActionResult NuevoPedido()
        {

            Conexion datos = new Conexion(_cache);
            Pedido pedido = new Pedido();
            pedido.Items.Add(new Item() { Id = 1 });

            ViewBag.Articulos = datos.CargarArticulos();

            return View(pedido);
        }



        [HttpGet]
        public ActionResult EditaPedido(string id)
        {

            Conexion datos = new Conexion(_cache);
            Pedido pedido = new Pedido();

            ViewBag.Articulos = datos.CargarArticulos();

            pedido = datos.PedidoArticulos(id);


            return View(pedido);
        }



        [HttpPost]
        public ActionResult NuevoPedido(Pedido pedido)
        {
            try
            {

                pedido.Items.RemoveAll(x => x.EsEliminado == true);

                double contador = 0;

                for (int i = 0; i < pedido.Items.Count; i++)
                {
                    contador += pedido.Items[i].CostoTotal; 


                    if (pedido.Items[i].articulo == null || pedido.Items[i].articulo.Length == 0)
                    {
                        contador -= pedido.Items[i].CostoTotal; 
                        pedido.Items.RemoveAt(i);
                    }
                }

                pedido.PrecioTotal = contador;


                Conexion datos = new Conexion(_cache);
                datos.addPedidos(pedido);
                ViewBag.MensajeExito = "El pedido ha sido guardado exitosamente ";
                ModelState.Clear();
                return RedirectToAction("NuevoPedido");


            }
            catch { return RedirectToAction("NuevoPedido"); }
        }


        [HttpPost]
        public ActionResult EditaPedido(Pedido pedido)
        {
            try
            {

                pedido.Items.RemoveAll(x => x.EsEliminado == true);

                int contador = 0;

                for (int i = 0; i < pedido.Items.Count; i++)
                {
                    contador += pedido.Items[i].Cantidad;


                    if (pedido.Items[i].articulo == null || pedido.Items[i].articulo.Length == 0)
                        {
                        pedido.Items.RemoveAt(i);
                        contador -= pedido.Items[i].Cantidad;

                    }
                }

                pedido.PrecioTotal = contador;

                Conexion datos = new Conexion(_cache);
                datos.ActualizarPedidos(pedido);


                ModelState.Clear();
                return RedirectToAction("ListaPedido");


            }
            catch { return RedirectToAction("ListaPedido"); }
        }


        public ActionResult ListaItems(string id)
        {
            Pedido tempPedido = new Pedido();
            Conexion datos = new Conexion(_cache);
            tempPedido = datos.PedidoArticulos(id);

            return View(tempPedido);
        }


        public ActionResult ListaPedido()
        {
            List<Pedido> tempList = new List<Pedido>();
            Conexion datos = new Conexion(_cache);
            tempList = datos.GetPedido();

            //return View(tempList);

            if (tempList.Count > 0 || tempList != null)
            {
                return View(tempList);
            }
            else
            {

                return View();
            }

        }

        [HttpGet]
        public ActionResult EliminaPedido(string id)
        {
            try
            {
                Conexion datos = new Conexion(_cache);
                List<Pedido> tempList = new List<Pedido>();
                tempList = datos.GetPedido();

                var pedido = tempList.FirstOrDefault(c => c.Id == id);

                if (pedido != null)
                {
                    datos.eliminaPedido(pedido);
                    TempData["mensaje"] = "Eliminado correctamente";
                    return RedirectToAction("ListaPedido");
                }


                TempData["mensaje"] = "No se ha podido eliminar";
                return RedirectToAction("ListaPedido");
            }
            catch
            {
                return RedirectToAction("ListaPedido");
            }
        }
    }
}
