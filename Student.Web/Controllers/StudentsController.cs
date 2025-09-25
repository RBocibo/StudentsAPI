using Microsoft.AspNetCore.Mvc;
using Students.Services.Interfaces;

namespace Students.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(Domain.Entities.Student student)
        {
            var cretaeStudent = await _studentService.CreateStudentAsync(student);
            return Ok(cretaeStudent);
        }

        [HttpDelete("{studentId}")]
        public async Task RemoveStudent(Guid studentId)
        {
            await _studentService.RemoveStudentAsync(studentId);
        }
    }
}
