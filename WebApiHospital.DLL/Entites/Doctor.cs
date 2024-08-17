using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiHospital.DLL.Entites
{
    public class Doctor:BaseEntity
    {
        public int Experience { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
