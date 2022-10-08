namespace TitanPhysiotherapy.Models.UserModels
{
    public class UserRegisterDto
    {
        public int id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public bool isPatient { get; set; } = false;
        public string Password { get; set; } = string.Empty;
        public int age { get; set; }
        public string email { get; set; } = string.Empty;
        public string contactNum { get; set; } = string.Empty;
    }
}
