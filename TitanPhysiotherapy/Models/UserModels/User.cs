using System.ComponentModel.DataAnnotations;

namespace TitanPhysiotherapy.Models.UserModels
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public bool IsPatient { get; set; }
        public string email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
