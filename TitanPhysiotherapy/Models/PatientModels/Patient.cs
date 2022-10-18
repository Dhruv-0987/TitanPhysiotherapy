using System.ComponentModel.DataAnnotations;

namespace TitanPhysiotherapy.Models.PatientModels
{
    public class Patient
    {
        [Key]
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string contactNum { get; set; }
        public string email { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }

    }
}
