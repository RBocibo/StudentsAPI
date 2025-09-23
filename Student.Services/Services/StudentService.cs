using Students.Domain.Generic;
using Students.Domain.Interfaces;
using Students.Services.Interfaces;

namespace Students.Services.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Student> CreateStudentAsync(Domain.Entities.Student student)
        {
            var createStudent = new Students.Domain.Entities.Student()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Phone = student.Phone,
                Gender = student.Gender,
            };

            await _studentRepository.AddAsync(createStudent);
            await _unitOfWork.CommitAsync();

            return createStudent;
        }

        public async Task<ICollection<Domain.Entities.Student>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            students.Take(3).ToList();

            if(students.Count < 3)
            {
                throw new Exception("Three or more  items need to be returned");
            }

            return students;
        }

        public async Task RemoveStudentAsync(Guid studentId)
        {
            var student = await _studentRepository.FindByExpressionAsyn(x =>x.StudentId == studentId);

            if(student == null)
            {
                throw new Exception($"The student with {studentId} does not exist.");
            }
            
            await _studentRepository.DeleteAsync(x => x.StudentId == studentId);
            await _unitOfWork.CommitAsync();
        }
    }
}
