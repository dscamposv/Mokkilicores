using Mokkilicores.Models;
namespace Mokkilicores.Interfaces
{
    public interface IConexion
    {
        public bool addArticulo(Articulo articulo);
        public List<Articulo> GetArticulo();
        public void editArticulo(Articulo articulo);
        public void eliminaArticulo(Articulo articulo);

    }
}