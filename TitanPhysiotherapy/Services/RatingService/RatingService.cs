using Microsoft.EntityFrameworkCore;
using TitanPhysiotherapy.Database;
using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Models.RatingModel;

namespace TitanPhysiotherapy.Services.RatingService
{
    public class RatingService : IRatingInterface
    {
        private readonly DataContext _context;
        public RatingService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<RatingModel>>> AddRating(RatingModel rating)
        {
            var ratingModel = new RatingModel();
            ratingModel.comment = rating.comment;
            ratingModel.Rating = rating.Rating;
            ratingModel.username = rating.username;

            _context.Rating.Add(ratingModel);
            await _context.SaveChangesAsync();

            var serviceResponse = new ServiceResponse<List<RatingModel>>();
            serviceResponse.Data = await _context.Rating.ToListAsync();
            serviceResponse.success = true;
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> GetAverageRating()
        {
            var ratingList = await _context.Rating.ToListAsync();
            int avgRating = 0;
            foreach (RatingModel rating in ratingList)
            {
                avgRating += rating.Rating;
            }
            avgRating /= ratingList.Count;
            var serviceResponse = new ServiceResponse<int>();
            serviceResponse.Data = avgRating;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<RatingModel>>> GetRatings()
        {
            var serviceResponse = new ServiceResponse<List<RatingModel>>();
            serviceResponse.Data = await _context.Rating.ToListAsync();
            serviceResponse.success = true;
            return serviceResponse;
        }
    }
}
