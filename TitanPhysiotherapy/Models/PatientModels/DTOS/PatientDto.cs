namespace TitanPhysiotherapy.Models.PatientModels.DTOS
{
    public class PatientDto
    {
        public int id { get; set; } = -1;
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public int age { get; set; } = 0;
        public string contactNum { get; set; } = string.Empty;
    }
}
