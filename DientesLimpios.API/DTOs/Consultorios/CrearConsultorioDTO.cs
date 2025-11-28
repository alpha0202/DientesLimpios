using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.DTOs.Consultorios
{
    public class CrearConsultorioDTO
    {

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }
    }
}
