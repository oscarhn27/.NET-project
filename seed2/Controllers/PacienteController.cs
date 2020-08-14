using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        [HttpGet("{id}")]
        public PacienteDTO GetOnePaciente([FromRoute] int idPaciente)
        {
            Paciente p = _pacienteService.GetPacienteById(idPaciente);
            return _mapper.Map<PacienteDTO>(p);
        }
        /*
        // POST api/<PacienteController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PacienteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PacienteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
