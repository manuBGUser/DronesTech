using AutoMapper;
using DronesTech.DTO;
using DronesTech.Interfaces;
using DronesTech.Models;
using DronesTech.Models.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DronesTech.Controllers
{
    [ApiController]
    [Route("api/DroneController")]
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

        [HttpGet("getDrones")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Drone>))]
        public IActionResult GetDrones()
        {
            var drones = _mapper.Map<List<DroneDTO>>(_droneRepository.GetDrones());
            if(!ModelState.IsValid)
                return BadRequest(new JsonResult("The drone isn't valid"));

            JsonResult result = new JsonResult(drones);
            result.StatusCode = 200;
            return Ok(result);
        }

        [HttpGet("getAbledDrones")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Drone>))]
        public IActionResult GetAbledDrones()
        {
            var drones = _mapper.Map<List<DroneDTO>>(_droneRepository.GetAbledDrones());
            if (!ModelState.IsValid)
                return BadRequest(new JsonResult("The drone isn't valid"));

            JsonResult result = new JsonResult(drones);
            result.StatusCode = 200;
            return Ok(result);
        }

        // POST: DroneController/Create
        [HttpPost("createDrone")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult CreateDrone([FromBody] DroneDTO droneDTO)
        {
            if (droneDTO == null)
                return BadRequest(new JsonResult("There is not incoming data"));

            var drone = _droneRepository.GetDroneBySerieNumber(droneDTO.SerieNumber);

            if (drone != null)
            {
                return UnprocessableEntity(new JsonResult("Drone already exits"));
            }

            if (!ModelState.IsValid)
                return BadRequest(new JsonResult("The drone isn't valid"));

            var droneMap = _mapper.Map<Drone>(droneDTO);
            if (!_droneRepository.CreateDrone(droneMap))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResult("Something went wrong while saving"));
            }

            JsonResult result = new JsonResult(droneMap);
            result.StatusCode = 200;
            return Ok(result);
        }

        [HttpGet("getDroneBattery/{droneId}")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetDroneBattery(int droneId)
        {
            if (!_droneRepository.DroneExists(droneId))
                return NotFound(new JsonResult("The drone doesn't exist"));
            var drone = _droneRepository.GetDroneById(droneId);
            if (!ModelState.IsValid)
                return BadRequest(new JsonResult("The drone isn't valid"));

            JsonResult result = new JsonResult(drone.BatteryCapacity);
            result.StatusCode = 200;
            return Ok(result);
        }

        [HttpPost("chargeMedicine/{droneId}")]
        [ProducesResponseType(200, Type = typeof(Drone))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ChargeMedicineToDrone(int droneId)
        {
            if (!_droneRepository.DroneExists(droneId))
                return NotFound(new JsonResult("The drone doesn't exist"));
            if (!_droneRepository.IsDroneEmpty(droneId))
                return BadRequest(new JsonResult("The drone isn't empty"));

            var drone = _droneRepository.GetDroneById(droneId);
            if (!ModelState.IsValid)
                return BadRequest(new JsonResult("The drone isn't valid"));
            if (!_droneRepository.GetAbledDrones().Contains(drone)) // the drone isnt able to charge
                return BadRequest(new JsonResult("The drone isn't abled to charge"));

            ICollection<Medicine> list = _medicineRepository.GetMedicinesToCharge(drone.WeightLimit);
            drone = _droneRepository.ChargeMedicines(drone, list);

            decimal medsWeight = drone.Medicines.Sum(m => m.Weight);
            JsonResult result = new JsonResult(drone);
            result.StatusCode = 200;
            return Ok(result);
        }

        [HttpPost("chargeMedicineToDroneByMedicines")]
        [ProducesResponseType(200, Type = typeof(Drone))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ChargeMedicineToDroneByMedicines([FromBody] Object optData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());
            int droneId = data.droneId;
            List<int> medicines = data.medicines.ToObject<List<int>>();
            
            if (!_droneRepository.DroneExists(droneId))
                return NotFound(new JsonResult("The drone doesn't exist"));
            if (!_droneRepository.IsDroneEmpty(droneId))
                return BadRequest(new JsonResult("The drone isn't empty"));

            var drone = _droneRepository.GetDroneById(droneId);
            if (!ModelState.IsValid)
                return BadRequest(new JsonResult("The drone isn't valid"));
            if (!_droneRepository.GetAbledDrones().Contains(drone)) // the drone isnt able to charge
                return BadRequest(new JsonResult("The drone isn't abled to charge"));

            ICollection<Medicine> list = _medicineRepository.GetMedicinesByIds(medicines);
            drone = _droneRepository.ChargeMedicines(drone, list);

            decimal medsWeight = drone.Medicines.Sum(m => m.Weight);
            JsonResult result = new JsonResult(drone);
            result.StatusCode = 200;
            return Ok(result);
        }

        [HttpGet("checkChargedMedsWeight/{droneId}")]
        [ProducesResponseType(200, Type = typeof(Drone))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CheckChargedMedsWeight(int droneId)
        {
            if (!_droneRepository.DroneExists(droneId))
                return NotFound(new JsonResult("The drone doesn't exist"));

            var drone = _droneRepository.GetDroneById(droneId);
            if (!ModelState.IsValid)
                return BadRequest(new JsonResult("The drone isn't valid"));
            if (drone.Status != StatusType.Charged || drone.Medicines == null) // the drone isn't charged
                return BadRequest(new JsonResult("The drone isn't charged"));

            decimal medsWeight = drone.Medicines.Sum(m => m.Weight);
            JsonResult result = new JsonResult(drone);
            result.StatusCode = 200;
            return Ok(result);
        }
    }
}
