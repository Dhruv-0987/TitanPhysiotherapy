using Microsoft.EntityFrameworkCore;
using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Models.PatientModels;
using TitanPhysiotherapy.Models.StaffModel;
using TitanPhysiotherapy.Models.UserModels;

namespace TitanPhysiotherapy.Database
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Treatment> Treatment { get; set; }
    }
}
