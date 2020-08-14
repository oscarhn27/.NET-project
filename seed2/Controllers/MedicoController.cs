using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Model.Entities;
using Repositories.Common;
using Services.Interfaces;

namespace seed2.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMedicoService _medicoService;
        public MedicoController(IMedicoService medicoService, IMapper mapper)
        {
            _medicoService = medicoService;
            _mapper = mapper;
        }

        [HttpGet]
        //[Transactional]
        public List<MedicoDTO> GetAllMedico() 
        {
            List<Medico> medicos = _medicoService.GetAllMedicos();
            List<MedicoDTO> mDTO = _mapper.Map<List<MedicoDTO>>(medicos);
            return mDTO;
        }

        [HttpGet("{id}")]
        public MedicoDTO GetOneMedico([FromRoute] int id)
        {
            Medico medico = _medicoService.GetMedicoById(id);
            return _mapper.Map<Medico, MedicoDTO>(medico);

        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddMedico([FromBody] Medico m)
        {
            _medicoService.AddMedico(m);
            return Created("El medico ha sido creado", m);
        }
        /*
        [HttpGet("[action]")]
        public DataCollection<MedicoDTO> PagedMedico()
        {
            DataCollection<MedicoDTO> medicos = _medicoService.GetPaged(1, 10);
            return medicos;

        }

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateMedico([FromBody] MedicoDTO medico)
        {
            _medicoService.Update(medico);
             return Ok();

        }


        


        [HttpDelete("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteMedico([FromBody] MedicoDTO medico)
        {
            _medicoService.Remove(medico);
            return Ok();

        }*/

    }
}