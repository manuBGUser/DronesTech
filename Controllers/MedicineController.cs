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
    public class MedicineController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMedicineRepository _medicineRepository;

        public MedicineController(IMedicineRepository medicineRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._medicineRepository = medicineRepository;
        }

        // POST: DroneController/Create
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult CreateMedicine([FromBody] MedicineDTO medicineDTO)
        {
            if (medicineDTO == null)
                return BadRequest(ModelState);

            var medicine = _medicineRepository.GetMedicines()
                .Where(m => m.Code == medicineDTO.Code).FirstOrDefault();

            if (medicine != null)
            {
                ModelState.AddModelError("", "medicine already exits");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var medicineMap = _mapper.Map<Medicine>(medicineDTO);
            if (!_medicineRepository.CreateMedicine(medicineMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(medicine);
        }

    }
}
