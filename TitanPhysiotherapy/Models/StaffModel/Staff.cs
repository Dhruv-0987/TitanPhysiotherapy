using System.ComponentModel.DataAnnotations;

namespace TitanPhysiotherapy.Models.StaffModel
{
    public class Staff
    {
        [Key]
        public int staffId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string contactNum { get; set; }
        public int userId { get; set; }
        public string clinicLocation { get; set; }
    }
}
