using System.ComponentModel.DataAnnotations;

namespace TitanPhysiotherapy.Models.RatingModel
{
    public class RatingModel
    {
        [Key]
        public int RatingId { get; set; }  
        public int Rating { get; set; } = 0;
        public string username { get; set; } = string.Empty;
        public string comment { get; set; } = string.Empty;
    }
}
