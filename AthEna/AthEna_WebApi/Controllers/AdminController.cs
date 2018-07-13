using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthEna_WebApi.Models;
using AthEna_WebApi.Repositories;
using AthEna_WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AthEna_WebApi.Controllers
{
    [Produces("application/json")]
    
    public class AdminController : Controller
    {
        private IConfiguration _config;
        private VehiclesRepository VehiclesRepo;
        private MetroStationRepository MetroStationRepo;
        public AdminController(IConfiguration Configuration)
        {            
            VehiclesRepo = new VehiclesRepository();
            MetroStationRepo = new MetroStationRepository();


            _config = Configuration;
        }

        [BasicAuthentication]
        [Route("api/Vehicles/{vehicleId?}")]
        [HttpGet]
        public IActionResult GetVehicles(Guid vehicleId)
        {
            try
            {
                // var vehicles may return as a list or just an object...
                var vehicles = VehiclesRepo.GetVehicle(vehicleId);
                return Ok(vehicles);                
            }
            catch (Exception e)
            {
                return StatusCode(500, _config["StatusCodesText:ServerErr"]);
            }
        }

        [BasicAuthentication]
        [Route("api/Vehicles")]
        [HttpPost]
        public IActionResult CreateVehicle([FromBody] Vehicle newVehicle)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var addNewVehicleResult = VehiclesRepo.CreateVehicle(newVehicle);
                    if (addNewVehicleResult.GetType() == typeof(Guid))
                        return Ok(addNewVehicleResult); //if the creation is successful return the id of the new card...
                    return BadRequest(); //if not... return bad request...
                }
                return BadRequest(ModelState);               
            }
            catch (Exception e)
            {
                return StatusCode(500, _config["StatusCodesText:ServerErr"]);
            }
        }



        [BasicAuthentication]
        [Route("api/MetroStations/{metroStationId?}")]
        [HttpGet]
        public IActionResult GetMetroStations(Guid metroStationId)
        {
            try
            {
                // var metroStations may return as a list or just an object...
                var metroStations = MetroStationRepo.GetMetroStation(metroStationId);
                return Ok(metroStations);
            }
            catch (Exception e)
            {
                return StatusCode(500, _config["StatusCodesText:ServerErr"]);
            }
        }

        [BasicAuthentication]
        [Route("api/MetroStations")]
        [HttpPost]
        public IActionResult CreateMetroStation([FromBody] MetroStation newMetroStation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var addNewMetroStationResult = MetroStationRepo.CreateMetroStation(newMetroStation);
                    if (addNewMetroStationResult.GetType() == typeof(Guid))
                        return Ok(addNewMetroStationResult); //if the creation is successful return the id of the new metro station...
                    return BadRequest(); //if not... return bad request...
                }
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                return StatusCode(500, _config["StatusCodesText:ServerErr"]);
            }
        }

    }
}