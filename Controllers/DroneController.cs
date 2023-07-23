using AutoMapper;
using DronesTech.DTO;
using DronesTech.Interfaces;
using DronesTech.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DronesTech.Controllers
{
    [ApiController]
    [Route("[api/controller]")]
    public class DroneController : Controller
    {
        private readonly IDroneRepository _droneRepository;
        private readonly IMapper _mapper;

        public DroneController(IDroneRepository droneRepository, IMapper mapper)
        {
            this._droneRepository = droneRepository;
            this._mapper = mapper;
        }

        // POST: DroneController/Create
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Drone>))]
        public IActionResult GetDrones()
        {
            var drones = _droneRepository.GetDrones();
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(drones);
        } 

        // POST: DroneController/Create
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult CreateDrone([FromBody] DroneDTO droneDTO)
        {
            if(droneDTO == null)
                return BadRequest(ModelState);

            var drone = _droneRepository.GetDrones()
                .Where(d => d.SerieNumber == droneDTO.SerieNumber).FirstOrDefault();

            if(drone != null)
            {
                ModelState.AddModelError("", "Category already exits");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var droneMap = _mapper.Map<Drone> (droneDTO);
            if(!_droneRepository.CreateDrone(droneMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(drone);
        }



    }
}
