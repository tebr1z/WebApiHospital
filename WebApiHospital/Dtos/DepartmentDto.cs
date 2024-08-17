namespace WebApiHospital.Dtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Limit { get; set; }
        public IFormFile ImageFile { get; set; }  
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }= DateTime.Now;
    }

}
