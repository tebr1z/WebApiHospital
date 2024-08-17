using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiHospital.DLL.Entites
{
    public class Department:BaseEntity
    {
        public int Limit { get; set; }
      
        public string? Image { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
