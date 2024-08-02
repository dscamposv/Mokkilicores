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


        public List<Articulo> ObtengaLaListaDeArticulos()
        {
            List<Articulo> resultado;

            if (EstaVacioElCache())
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


        public bool EstaVacioElCache()
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
    }
}
