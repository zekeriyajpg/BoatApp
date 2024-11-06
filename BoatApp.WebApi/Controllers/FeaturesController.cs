using BoatApp.Business.Operations.Feature;
using BoatApp.Business.Operations.Feature.Dtos;
using BoatApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoatApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeaturesController( IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpPost]
        [Authorize(Roles = " Admin ")]
        public async Task<IActionResult> AddFeature(FeatureRequest request)
        {
            var addFeatureDto = new AddFeatureDto
            { 
                Title = request.Title 
            };

            var result = await _featureService.AddFeature(addFeatureDto);

            if (result.IsSucced)
                return Ok();
            else 
                return BadRequest(result.Message);
        }
    }
}
