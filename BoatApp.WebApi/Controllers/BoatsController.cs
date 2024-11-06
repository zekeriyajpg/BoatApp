using BoatApp.Business.Operations.Boat;
using BoatApp.Business.Operations.Boat.Dtos;
using BoatApp.WebApi.Filters;
using BoatApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoatApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoatsController : ControllerBase
    {
        private readonly IBoatService _boatService;

        public BoatsController(IBoatService boatService)
        {
            _boatService = boatService;
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetBoat(int id)
        {
            var boat = await _boatService.GetBoat(id);
            if (boat is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(boat);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetBoats()
        {
            var boats = await _boatService.GetBoats();
            return Ok(boats);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBoat(AddBoatRequest request)
        {
            var addBoatDto = new AddBoatDto
            {
                Name = request.Name,
                Model = request.Model,
                Price = request.Price,
                BoatTypes = request.BoatTypes,
                FeatureIds = request.FeatureIds
            };
            var result = await _boatService.AddBoat(addBoatDto);

            if (!result.IsSucced)
            {
                return BadRequest(result.Message);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPatch("{id}/price")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AdjustPrices(int id, int changeTo)
        {
           var result = await _boatService.AdjustPrices(id, changeTo);
            if (!result.IsSucced) 
                return NotFound();
            else
                return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBoat(int id)
        {
            var result = await _boatService.DeleteBoat(id);
            if (!result.IsSucced)
                return NotFound();
            else
                return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [TimeControlFilter]
        public async Task<IActionResult> UpdateBoat(int id, UpdateBoatRequest request)
        {
            var updateBoatDto = new UpdateBoatDto
            {
                Id = id,
                Name = request.Name,
                Model = request.Model,
                Price = request.Price,
                BoatType = request.BoatType,
                FeatureIds = request.FeatureIds
            };

            var result = await _boatService.UpdateBoat(updateBoatDto);

            if (!result.IsSucced)
            {
                return NotFound(result.Message);
            }
            else
            {
                return await GetBoat(id);
            }
        }
    }
}
