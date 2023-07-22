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

        public DroneController(IDroneRepository droneRepository)
        {
            this._droneRepository = droneRepository;
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
            try
            {
                //var data = JsonConvert.DeserializeObject<DroneDTO>(droneDTO.ToString());

                //Guid user = new Guid(data.user.ToString());
                //int project = int.Parse(data.project.ToString());
                //string description = data.description.ToString();

                //Bug bug = new Bug()
                //{
                //    ProjectId = project,
                //    User = _context.Users.Find(data.user.ToString()),
                //    Description = description,
                //    CreationDate = DateTime.Now,
                //};

                //_context.Add(bug);
                //_context.SaveChangesAsync();

                //return Json
            }
            catch
            {
                return View();
            }
        }

        // GET: DroneController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DroneController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DroneController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DroneController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
