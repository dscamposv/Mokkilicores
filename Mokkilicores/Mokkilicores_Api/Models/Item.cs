using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Mokkilicores.Models
{
    public class Item
    {
        [Required(ErrorMessage = "Campo Obligatorio")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string articulo { get; set; }
        
        [Required(ErrorMessage = "Campo Obligatorio")]
        public int Cantidad { get; set; }
        
        [Required(ErrorMessage = "Campo Obligatorio")]
        public double Costo { get; set; }
        
        public double CostoTotal { get; set; }

        [NotMapped]
        public bool EsEliminado { get; set; }

    }
}
