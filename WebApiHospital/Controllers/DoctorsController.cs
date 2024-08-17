
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class DoctorsController : ControllerBase
    {
        private readonly WebApiHospitalContext _context;
        private readonly IMapper _mapper;

        public DoctorsController(WebApiHospitalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors()
        {
            var doctors = await _context.Doctors.Include(d => d.Department).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<DoctorDto>>(doctors));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors.Include(d => d.Department)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (doctor == null) return NotFound();

            return Ok(_mapper.Map<DoctorDto>(doctor));
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDto>> CreateDoctor(DoctorDto doctorDto)
        {
            var doctor = _mapper.Map<Doctor>(doctorDto);
            _context.Doctors.Add(doctor);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDoctor), new { id = doctor.Id }, _mapper.Map<DoctorDto>(doctor));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, DoctorDto doctorDto)
        {
            if (id != doctorDto.Id) return BadRequest();

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return NotFound();

            _mapper.Map(doctorDto, doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return NotFound();

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
       
        [HttpGet]
        [Route("api/Departments")]
        public async Task<ActionResult<PagedResult<DoctorDto>>> GetDoctors([FromQuery] PaginationParams paginationParams)
        {
            var query = _context.Doctors.Include(d => d.Department).AsQueryable();

            var doctors = await query
                .ProjectTo<DoctorDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(paginationParams.PageNumber, paginationParams.PageSize);

            return Ok(doctors);
        }

    }

}
