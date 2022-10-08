using System.ComponentModel.DataAnnotations;

namespace TitanPhysiotherapy.Models
{
    public class Treatment
    {
        [Key]
        public int treatmentId { get; set; } = 0;
        public int staffId { get; set; } = 0;
        public int patientId { get; set; } = 0;
        public string description { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.MinValue;
    }
}
