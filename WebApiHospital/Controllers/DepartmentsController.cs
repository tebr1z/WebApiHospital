using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiHospital.DLL.Data;
using WebApiHospital.DLL.Entites;
using WebApiHospital.Dtos;
using WebApiHospital.Extensions;

namespace WebApiHospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class DepartmentsController : ControllerBase
    {
       
        private readonly WebApiHospitalContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
       
        public DepartmentsController(WebApiHospitalContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
        {
            var departments = await _context.Departments.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<DepartmentDto>>(departments));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            return Ok(_mapper.Map<DepartmentDto>(department));
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> CreateDepartment([FromForm] DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);

            if (departmentDto.ImageFile != null)
            {
                var imagePath = Path.Combine(_environment.WebRootPath, "images", departmentDto.ImageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await departmentDto.ImageFile.CopyToAsync(stream);
                }
                department.Image = $"/images/{departmentDto.ImageFile.FileName}";
            }

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, _mapper.Map<DepartmentDto>(department));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromForm] DepartmentDto departmentDto)
        {
            if (id != departmentDto.Id) return BadRequest();

            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            _mapper.Map(departmentDto, department);

            if (departmentDto.ImageFile != null)
            {
                var imagePath = Path.Combine(_environment.WebRootPath, "images", departmentDto.ImageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await departmentDto.ImageFile.CopyToAsync(stream);
                }
                department.Image = $"/images/{departmentDto.ImageFile.FileName}";
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        [HttpGet]
        [Route("api/Departments")]
        public async Task<ActionResult<PagedResult<DepartmentDto>>> GetDepartments([FromQuery] PaginationParams paginationParams)
        {
            var query = _context.Departments.AsQueryable();

            var departments = await query
                .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(paginationParams.PageNumber, paginationParams.PageSize);

            return Ok(departments);
        }


    }


}
