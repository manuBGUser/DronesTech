using AutoMapper;
using DronesTech.DTO;
using DronesTech.Interfaces;
using DronesTech.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace DronesTech.Controllers
{
    [ApiController]
    [Route("api/medicineController")]
    public class MedicineController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMedicineRepository _medicineRepository;

        public MedicineController(IMedicineRepository medicineRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._medicineRepository = medicineRepository;
        }

        // POST: MedicineController/Create
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult CreateMedicine([FromBody] MedicineDTO medicineDTO)
        {
            if (medicineDTO == null)
                return BadRequest(new JsonResult("There is not incoming data"));

            var medicine = _medicineRepository.GetMedicines()
                .Where(m => m.Code == medicineDTO.Code).FirstOrDefault();

            if (medicine != null)
            {
                return UnprocessableEntity(new JsonResult("Medicine already exits"));
            }

            if (!ModelState.IsValid)
                return BadRequest(new JsonResult("The drone isn't valid"));

            var medicineMap = _mapper.Map<Medicine>(medicineDTO);
            if (!_medicineRepository.CreateMedicine(medicineMap))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResult("Something went wrong while saving"));
            }

            return Ok(new JsonResult(medicine));
        }

    }
}
