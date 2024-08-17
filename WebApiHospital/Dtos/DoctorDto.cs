namespace WebApiHospital.Dtos
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
