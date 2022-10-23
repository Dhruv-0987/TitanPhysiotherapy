using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Models.RatingModel;

namespace TitanPhysiotherapy.Services.RatingService
{
    public interface IRatingInterface
    {
        public Task<ServiceResponse<List<RatingModel>>> AddRating(RatingModel rating);
        public Task<ServiceResponse<List<RatingModel>>> GetRatings();
        public Task<ServiceResponse<int>> GetAverageRating();
    }
}
