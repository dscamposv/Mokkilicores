using Microsoft.Extensions.Caching.Memory;
using Mokkilicores.Models;

namespace Mokkilicores.Data
{
    public class Conexion
    {
        private readonly IMemoryCache _cache;

        public Conexion(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void addArticulo(Articulo articulo)
        {
            List<Articulo> tempArticulo = GetArticulo();
            tempArticulo.Add(articulo);

        }

        public List<Articulo> GetArticulo()
        {

            List<Articulo> tempList;

            if (_cache.Get("ListaArticulos") == null)
            {
                tempList = new List<Articulo>();
                _cache.Set("ListaArticulos", tempList);
            }
            else
            {
                tempList = (List<Articulo>)_cache.Get("ListaArticulos");
            }

            return tempList;
        }

        public void editArticulo(Articulo articulo)
        {
            var listaActualCache = GetArticulo();
            var articuloEdit = listaActualCache.FirstOrDefault(c => c.Id == articulo.Id);
            var index = listaActualCache.FindIndex(c => c.Id == articulo.Id);

            listaActualCache.Remove(articuloEdit);
            listaActualCache.Insert(index, articulo);

            _cache.Remove("ListaArticulos");
            _cache.Set("ListaArticulos", listaActualCache);
        }

        public void eliminaArticulo(Articulo articulo)
        {
            var listaActualCache = GetArticulo();
            var articuloEliminar = listaActualCache.FirstOrDefault(c => c.Id == articulo.Id);

            listaActualCache.Remove(articuloEliminar);

            _cache.Remove("ListaArticulos");
            _cache.Set("ListaArticulos", listaActualCache);
        }

        public void addCliente(Cliente cliente)
        {
            List<Cliente> tempCliente = GetCliente();
            tempCliente.Add(cliente);

        }

        public List<Cliente> GetCliente()
        {

            List<Cliente> tempList;

            if (_cache.Get("ListaCliente") == null)
            {
                tempList = new List<Cliente>();
                _cache.Set("ListaCliente", tempList);
            }
            else
            {
                tempList = (List<Cliente>)_cache.Get("ListaCliente");
            }

            return tempList;
        }

        public void editCliente(Cliente cliente)
        {
            var listaActualCache = GetCliente();
            var clienteEdit = listaActualCache.FirstOrDefault(c => c.Id == cliente.Id);
            var index = listaActualCache.FindIndex(c => c.Id == cliente.Id);

            listaActualCache.Remove(clienteEdit);
            listaActualCache.Insert(index, cliente);

            _cache.Remove("ListaCliente");
            _cache.Set("ListaCliente", listaActualCache);
        }

        public void eliminaCliente(Cliente cliente)
        {
            var listaActualCache = GetCliente();
            var clienteEliminar = listaActualCache.FirstOrDefault(c => c.Id == cliente.Id);

            listaActualCache.Remove(clienteEliminar);

            _cache.Remove("ListaCliente");
            _cache.Set("ListaCliente", listaActualCache);
        }


        public void addPedidos(Pedido pedido)
        {

            List<Pedido> tempPedido = GetPedido();
            tempPedido.Add(pedido);
        }



        public void ActualizarPedidos(Pedido pedido)
        {
            var ListaActualCache = _cache.Get("ListaPedido") as List<Pedido>;
            var ListaRespaldoCache = ListaActualCache.FirstOrDefault(x => x.Id == pedido.Id);
            var ListRespaldoCacheIndex = ListaActualCache.FindIndex(x => x.Id == pedido.Id);

            ListaActualCache.Remove(ListaRespaldoCache);
            ListaActualCache.Insert(ListRespaldoCacheIndex, pedido);

            _cache.Remove("ListaPedido");
            _cache.Set("ListaPedido", ListaActualCache);


        }

        public Pedido PedidoArticulos(string id)
        {
            Pedido newPedido = new Pedido();

            foreach (Pedido pedido in GetPedido())
            {
                if (pedido.Id == id)
                {
                    newPedido.Id = pedido.Id;
                    newPedido.Estado = pedido.Estado;
                    newPedido.Items = pedido.Items;
                }
            }

            return newPedido;

        }

        public List<Pedido> GetPedido()
        {

            List<Pedido> tempList;

            if (_cache.Get("ListaPedido") == null)
            {
                tempList = new List<Pedido>();

                _cache.Set("ListaPedido", tempList);
            }
            else
            {
                tempList = (List<Pedido>)_cache.Get("ListaPedido");
            }
            return tempList;
        }

        public List<Articulo> CargarArticulos()
        {
            List<Articulo> tempList = new List<Articulo>();
            tempList = GetArticulo();

            return tempList;
        }

        public void eliminaPedido(Pedido pedido)
        {
            var listaActualCache = GetPedido();
            var pedidoEliminar = listaActualCache.FirstOrDefault(c => c.Id == pedido.Id);

            listaActualCache.Remove(pedidoEliminar);

            _cache.Remove("ListaPedido");
            _cache.Set("ListaPedido", listaActualCache);
        }
    }
}
