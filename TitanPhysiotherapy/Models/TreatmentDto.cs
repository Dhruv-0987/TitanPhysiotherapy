namespace TitanPhysiotherapy.Models
{
    public class TreatmentDto
    {
        public int treatmentId { get; set; } = 0;
        public int staffId { get; set; } = 0;
        public int patientId { get; set; } = 0;
        public string description { get; set; } = string.Empty;
        public string DateTime { get; set; } = String.Empty;
        public string staffName { get; set; } = string.Empty;
        public string clinicLocation { get; set; } = string.Empty;
    }
}
