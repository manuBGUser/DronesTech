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
    [Route("api/controller")]
    public class DroneController : Controller
    {
        private readonly IDroneRepository _droneRepository;
        private readonly IMapper _mapper;
        private readonly IMedicineRepository _medicineRepository;

        public DroneController(IDroneRepository droneRepository, IMapper mapper, IMedicineRepository medicineRepository)
        {
            this._droneRepository = droneRepository;
            this._mapper = mapper;
            this._medicineRepository = medicineRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Drone>))]
        public IActionResult GetDrones()
        {
            var drones = _mapper.Map<List<DroneDTO>>(_droneRepository.GetDrones());
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(drones);
        }

        [HttpGet("/getAbleDrones")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Drone>))]
        public IActionResult GetAbleDrones()
        {
            var drones = _mapper.Map<List<DroneDTO>>(_droneRepository.GetAbleDrones());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(drones);
        }

        // POST: DroneController/Create
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult CreateDrone([FromBody] DroneDTO droneDTO)
        {
            if (droneDTO == null)
                return BadRequest(ModelState);

            var drone = _droneRepository.GetDrones()
                .Where(d => d.SerieNumber == droneDTO.SerieNumber).FirstOrDefault();

            if (drone != null)
            {
                ModelState.AddModelError("", "Category already exits");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var droneMap = _mapper.Map<Drone>(droneDTO);
            if (!_droneRepository.CreateDrone(droneMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(drone);
        }

        [HttpGet("{droneId}")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetDroneBatery(int droneId)
        {
            if (!_droneRepository.DroneExists(droneId))
                return BadRequest(ModelState);
            var drone = _droneRepository.GetDroneById(droneId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(drone.BatteryCapacity);
        }

        [HttpGet("{droneId}")]
        [ProducesResponseType(200, Type = typeof(Drone))]
        [ProducesResponseType(400)]
        public IActionResult ChargeMedicineToDrone(int droneId)
        {
            if (!_droneRepository.DroneExists(droneId))
                return BadRequest(ModelState);
            if(!_droneRepository.IsDroneEmpty(droneId))
                return BadRequest(ModelState);

            var drone = _droneRepository.GetDroneById(droneId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ICollection<Medicine> list = _medicineRepository.GetMedicinesToCharge(drone.WeightLimit);
            drone = _droneRepository.ChargeMedicines(drone, list);

            return Ok(drone);
        }
    }
}
