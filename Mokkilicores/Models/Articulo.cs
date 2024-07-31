using System.ComponentModel.DataAnnotations;
namespace Mokkilicores.Models
{
    public class Articulo
    {
        [Required (ErrorMessage ="Campo Obligatorio")]
        //[RegularExpression(@"^\\d+$", ErrorMessage = "El Id solo puede contener números")]
        public string Id { get; set; }


        [Required(ErrorMessage = "Campo Obligatorio")]
        //[RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "La cantidad de artículos solo puede contener números y puntos decimales")]
        public int Cantidad { get; set; }


        [Required(ErrorMessage = "Campo Obligatorio")]
        //[RegularExpression(@"^\\d+$", ErrorMessage = "El Id de bodega solo puede contener números")]
        public int IdBodega { get; set; }


        [Required(ErrorMessage = "Campo Obligatorio")]
        //[RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])\\(0[1-9]|1[012])\\(19|20)\d\d$", ErrorMessage = "La fecha debe tener el formato dd\\mm\\aaaa")]
        public DateTime FechaIngreso { get; set; }


        [Required(ErrorMessage = "Campo Obligatorio")]
        //[RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])\\(0[1-9]|1[012])\\(19|20)\d\d$", ErrorMessage = "La fecha debe tener el formato dd\\mm\\aaaa")]
        public DateTime FechaVence { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public string TipoLicor { get; set; }

    }
}
