using System.ComponentModel.DataAnnotations;
namespace Mokkilicores.Models
{
    public class Pedido
    {
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Id { get; set; }

        public string Estado { get; set; }

        public double PrecioTotal { get; set; }

        public virtual List<Item> Items { get; set; } = new List<Item>();

    }
}
