using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Model.Entities;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Aquaservice.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPacienteService _pacienteService;

        public PacienteController(IMapper mapper, IPacienteService pacienteService)
        {
            _mapper = mapper;
            _pacienteService = pacienteService;
        }
        /*
        // GET: api/<PacienteController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        [HttpGet]
        //[Transactional]
        public List<PacienteDTO> GetAllMedico()
        {
            List<Paciente> paciente = _pacienteService.GetAllPaciente();
            List<PacienteDTO> pDTO = _mapper.Map<List<PacienteDTO>>(paciente);
            return pDTO;
        }

        [HttpGet("{IdPaciente}")]
        public PacienteDTO GetOnePaciente([FromRoute] int IdPaciente)
        {
            Paciente p = _pacienteService.GetPacienteById(IdPaciente);
            return _mapper.Map<PacienteDTO>(p);
        }
        
        // POST Paciente/AddPaciente
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddPaciente([FromBody] Paciente p)
        {
            _pacienteService.AddPaciente(p);
            return Created("El paciente ha sido creado", p);
        }

        // PUT: /Paciente/UpdatePaciente
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult UpdatePaciente([FromBody] Paciente p)
        {
            _pacienteService.UpdatePaciente(p);
            return Accepted(p);

        }

        // DELETE: /Paciente/DeletePaciente
        [HttpDelete("[action]/{IdPaciente}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult DeletePaciente([FromRoute] int IdPaciente)
        {
            _pacienteService.DeletePacienteById(IdPaciente);
            return Accepted();

        }
    }
}
