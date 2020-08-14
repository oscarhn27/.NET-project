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

        // GET: /Medico
        [HttpGet]
        public List<MedicoDTO> GetAllMedico() 
        {
            List<Medico> medicos = _medicoService.GetAllMedicos();
            List<MedicoDTO> mDTO = _mapper.Map<List<MedicoDTO>>(medicos);
            return mDTO;
        }

        // GET: /Medico/{idMedico}
        [HttpGet("{idMedico}")]
        public MedicoDTO GetOneMedico([FromRoute] int idMedico)
        {
            Medico medico = _medicoService.GetMedicoById(idMedico);
            return _mapper.Map<Medico, MedicoDTO>(medico);

        }

        // POST: /Medico/AddMedico
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AddMedico([FromBody] Medico m)
        {
            try
            {
                _medicoService.AddMedico(m);
            } catch(NHibernate.Exceptions.GenericADOException e)
            {
                return Conflict(e.Message);
            }
            
            return Created("El medico ha sido creado", m);
        }

        // PUT: /Medico/UpdateMedico
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateMedico([FromBody] Medico m)
        {
            try
            {
                _medicoService.UpdateMedico(m);
            }
            catch(NHibernate.StaleObjectStateException e)
            {
                return NotFound("El medico a modificar no se encontro");
            }            
            return Accepted(m);

        }

        // DELETE: /Medico/DeleteMedico
        [HttpDelete("[action]/{IdMedico}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeleteMedico([FromRoute] int IdMedico)
        {
            try
            {
                _medicoService.DeleteMedicoById(IdMedico);
            } catch(Exception e)
            {
                return Conflict(e.Message);
            }
            return Accepted();

        }
    }
}