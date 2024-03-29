﻿using AutoMapper;
using DronesTech.DTO;
using DronesTech.Interfaces;
using DronesTech.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace DronesTech.Controllers
{
    [ApiController]
    [Route("api/MedicineController")]
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
        [HttpPost("createMedicine")]
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

            if (!Regex.IsMatch(medicineDTO.Name, @"^[a-zA-Z0-9_-]+$"))
                return BadRequest(new JsonResult("The drone name can only have letters, number, dashes and underscores"));
            if (!Regex.IsMatch(medicineDTO.Name, @"^[A-Z0-9_]+$"))
                return BadRequest(new JsonResult("The drone name can only have capital letters, number and dashes"));
            if (!ModelState.IsValid)
                return BadRequest(new JsonResult("The drone isn't valid"));

            var medicineMap = _mapper.Map<Medicine>(medicineDTO);
            if (!_medicineRepository.CreateMedicine(medicineMap))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResult("Something went wrong while saving"));
            }

            JsonResult result = new JsonResult(medicineMap);
            result.StatusCode = 200;
            return Ok(result);
        }

    }
}
