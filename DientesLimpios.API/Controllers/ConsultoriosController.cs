using DientesLimpios.API.DTOs.Consultorios;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.ObtenerDetalleConsulta;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultoriosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsultoriosController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<ConsultorioListadoDTO>>> GetAll()
        {
            var consulta = new ConsultaObtenerListadoConsultorios();
            var resultado = await _mediator.Send(consulta);
            return resultado;
        }



        [HttpGet("{id}")]

        public async Task<ActionResult<ConsultorioDetalleDTO>> GetById(Guid id)
        {
            var consulta = new ConsultaObtenerDetalleConsultorio { Id = id };
            var resultado = await _mediator.Send(consulta);
            return resultado;
        }




        [HttpPost]
        public async Task<IActionResult> Post(CrearConsultorioDTO crearConsultorioDTO)
        {
            var comando = new ComandoCrearConsultorios { Nombre = crearConsultorioDTO.Nombre };
           await _mediator.Send(comando);
            return Ok();
        }




    }
}
