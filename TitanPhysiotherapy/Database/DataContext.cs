using Microsoft.EntityFrameworkCore;
using TitanPhysiotherapy.Models.PatientModels;

namespace TitanPhysiotherapy.Database
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }

        
    }
}
