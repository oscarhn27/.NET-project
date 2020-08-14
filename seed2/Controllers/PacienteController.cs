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

        // GET /Paciente
        [HttpGet]
        public List<PacienteDTO> GetAllMedico()
        {
            List<Paciente> paciente = _pacienteService.GetAllPaciente();
            List<PacienteDTO> pDTO = _mapper.Map<List<PacienteDTO>>(paciente);
            return pDTO;
        }

        // GET /Paciente/{IdPaciente}
        [HttpGet("{IdPaciente}")]
        public PacienteDTO GetOnePaciente([FromRoute] int IdPaciente)
        {
            Paciente p = _pacienteService.GetPacienteById(IdPaciente);
            return _mapper.Map<PacienteDTO>(p);
        }
        
        // POST /Paciente/AddPaciente
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AddPaciente([FromBody] Paciente p)
        {
            try 
            { 
                _pacienteService.AddPaciente(p);
            } catch(NHibernate.Exceptions.GenericADOException e)
            {
                return Conflict(e.Message);
            }
            return Created("El paciente ha sido creado", p);
        }

        // PUT: /Paciente/UpdatePaciente
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePaciente([FromBody] Paciente p)
        {
            try
            {
                _pacienteService.UpdatePaciente(p);
            }
            catch (NHibernate.StaleObjectStateException e)
            {
                return NotFound("El medico a modificar no se encontro");
            }
            return Accepted(p);
        }

        // DELETE: /Paciente/DeletePaciente
        [HttpDelete("[action]/{IdPaciente}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeletePaciente([FromRoute] int IdPaciente)
        {
            try
            {
                _pacienteService.DeletePacienteById(IdPaciente);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
            return Accepted();

        }
    }
}
