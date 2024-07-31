using System.ComponentModel.DataAnnotations;
namespace Mokkilicores.Models
{
    public class Cliente
    {
        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression("^(0\\d{1}-\\d{4}-\\d{4})$", ErrorMessage = "Formato Fisico 0#-####-####")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Provincia { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Canton { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Distrito { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public double CompraSemestral { get; set; }
        public double CompraAnual { get; set; }

        public double CompraTotal { get; set; }
        public string? PassWord { get; set; }

    }
}
