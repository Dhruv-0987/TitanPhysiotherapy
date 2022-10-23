using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Models.RatingModel;
using TitanPhysiotherapy.Services.RatingService;

namespace TitanPhysiotherapy.Controllers
{
    [Route("api/Rating")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingInterface _ratingService;
        public RatingController(IRatingInterface RatingService)
        {
            _ratingService = RatingService;
        }

        [HttpGet("GetAllRatings")]
        public async Task<ActionResult<ServiceResponse<List<RatingModel>>>> GetAllRatings()
        {
            return Ok(await _ratingService.GetRatings());
        }

        [HttpPost("AddRating")]
        public async Task<ActionResult<ServiceResponse<List<RatingModel>>>> AddRating(RatingModel rating)
        {
            return Ok(await _ratingService.AddRating(rating));
        }

        [HttpGet("GetAverageRating")]
        public async Task<ActionResult<ServiceResponse<int>>> AddRating()
        {
            return Ok(await _ratingService.GetAverageRating());
        }
    }
}
