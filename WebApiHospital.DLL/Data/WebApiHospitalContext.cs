using Microsoft.EntityFrameworkCore;

using WebApiHospital.DLL.Entites;
using System.Reflection;
namespace WebApiHospital.DLL.Data
{
    public class WebApiHospitalContext:DbContext
    {
    
        public WebApiHospitalContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>()
                .Ignore(d => d.ImageFile); 
        }

    }


}
