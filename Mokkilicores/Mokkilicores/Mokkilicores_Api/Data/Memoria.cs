using Microsoft.Extensions.Caching.Memory;
using Mokkilicores.Models;

namespace Mokkilicores_api.Data
{
    public class Memoria
    {
        private readonly IMemoryCache ElCache;
        public Memoria(IMemoryCache cache)
        {
            ElCache = cache;
        }

        //ARTICULOS
        public List<Articulo> ObtengaLaListaDeArticulos()
        {
            List<Articulo> resultado;

            if (EstaVacioElCacheArticulo())
            {
                resultado = new List<Articulo>();
                ElCache.Set("ListaDeArticulos", resultado);
            }
            else
                resultado = (List<Articulo>)ElCache.Get("ListaDeArticulos");

            return resultado;
        }

        public Articulo ObtengaArticulo(int id)
        {

            List<Articulo> laLista;
            laLista = ObtengaLaListaDeArticulos();

            foreach (Articulo articulo in laLista)
            {
                if (articulo.Id == id)
                    return articulo;
            }

            return null;

        }


        public bool EstaVacioElCacheArticulo()
        {
            if (ElCache.Get("ListaDeArticulos") is null)
                return true;
            else
                return false;
        }

        public bool AgregarArticulo(Articulo articulo)
        {
            List<Articulo> laLista;
            laLista = ObtengaLaListaDeArticulos();
            laLista.Add(articulo);
            return true;
        }

        public bool EliminarArticulo(int id)
        {
            List<Articulo> laLista;
            laLista = ObtengaLaListaDeArticulos();
            Articulo articulo = ObtengaArticulo(id);
            if (articulo is null)
                return false;
            else
            {
                laLista.Remove(articulo);
                return true;
            }
        }
        public bool EditarArticulo(Articulo articulo)
        {
            List<Articulo> laLista;
            laLista = ObtengaLaListaDeArticulos();
            Articulo articuloViejo = ObtengaArticulo(articulo.Id);
            if (articuloViejo is null)
                return false;
            else
            {
                laLista.Remove(articuloViejo);
                laLista.Add(articulo);
                return true;
            }
        }


        //CLIENTES

        public List<Cliente> ObtengaLaListaDeClientes()
        {
            List<Cliente> resultado;

            if (EstaVacioElCacheCliente())
            {
                resultado = new List<Cliente>();
                ElCache.Set("ListaDeClientes", resultado);
            }
            else
                resultado = (List<Cliente>)ElCache.Get("ListaDeClientes");

            return resultado;
        }

        public Cliente ObtengaCliente(string id)
        {

            List<Cliente> laLista;
            laLista = ObtengaLaListaDeClientes();

            foreach (Cliente cliente in laLista)
            {
                if (cliente.Id == id)
                    return cliente;
            }

            return null;

        }


        public bool EstaVacioElCacheCliente()
        {
            if (ElCache.Get("ListaDeClientes") is null)
                return true;
            else
                return false;
        }

        public bool AgregarCliente(Cliente cliente)
        {
            List<Cliente> laLista;
            laLista = ObtengaLaListaDeClientes();
            laLista.Add(cliente);
            return true;
        }

        public bool EliminarCliente(string id)
        {
            List<Cliente> laLista;
            laLista = ObtengaLaListaDeClientes();
            Cliente cliente = ObtengaCliente(id);
            if (cliente is null)
                return false;
            else
            {
                laLista.Remove(cliente);
                return true;
            }
        }
        public bool EditarCliente(Cliente cliente)
        {
            List<Cliente> laLista;
            laLista = ObtengaLaListaDeClientes();
            Cliente clienteViejo = ObtengaCliente(cliente.Id);
            if (clienteViejo is null)
                return false;
            else
            {
                laLista.Remove(clienteViejo);
                laLista.Add(cliente);
                return true;
            }
        }



    }
}
