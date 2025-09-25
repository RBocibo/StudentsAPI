using Moq;
using Students.Domain.Generic;
using Students.Domain.Interfaces;
using Students.Services.Interfaces;
using Students.Services.Services;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Students.UnitTests
{
    public class ServicesUnitTests
    {
        private readonly Mock<IStudentRepository> _studentRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public ServicesUnitTests()
        {
            _studentRepositoryMock = new Mock<IStudentRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task CreateStudentAsync_Success()
        {
            // Arrange

            var student = new Domain.Entities.Student
            {
                StudentId = Guid.NewGuid(),
                FirstName = "Rose",
                LastName = "Bocibo",
                Email = "rose@gmail.com",
                Phone = "0123456789",
                Gender = Student.Models.Models.GenderEnum.Female
            };

            _studentRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Domain.Entities.Student>())).ReturnsAsync(student);
            _unitOfWorkMock.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);

            var addStudent = new StudentService(_studentRepositoryMock.Object, _unitOfWorkMock.Object);

            // Act

            var result = await addStudent.CreateStudentAsync(student);

            // Assert

            _studentRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Domain.Entities.Student>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);

            Assert.Equal(student.FirstName, result.FirstName);
            Assert.Equal(student.LastName, result.LastName);
            Assert.Equal(student.Email, result.Email);
            Assert.Equal(student.Phone, result.Phone);
            Assert.Equal(student.Gender, result.Gender);
        }


        [Fact]
        public async Task GetAllStudentsAsync_Success()
        {
            //Arrange

            var students = new List<Domain.Entities.Student>()
            {
                new Domain.Entities.Student()
                {
                    StudentId = Guid.NewGuid(),
                    FirstName = "StudentName1",
                    LastName = "StudentSurname1",
                    Email = "StudentSurname1@gmail.com",
                    Gender = Student.Models.Models.GenderEnum.Female,
                    Phone = "0123456789"
                },

                new Domain.Entities.Student()
                {
                    StudentId = Guid.NewGuid(),
                    FirstName = "StudentName2",
                    LastName = "StudentSurname2",
                    Email = "StudentSurname2@gmail.com",
                    Gender = Student.Models.Models.GenderEnum.Female,
                    Phone = "0123456749"
                },

                new Domain.Entities.Student()
                {
                    StudentId = Guid.NewGuid(),
                    FirstName = "StudentName3",
                    LastName = "StudentSurname3",
                    Email = "StudentSurname3@gmail.com",
                    Gender = Student.Models.Models.GenderEnum.Male,
                    Phone = "0124456789"
                }
            };

            _studentRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(students);
            var getAllStudents = new StudentService(_studentRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act

            var result = await getAllStudents.GetAllStudentsAsync();

            //Assert

            Assert.Equal(students, result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetAllStudentsAsync_Fail()
        {
            //Arrange

            var students = new List<Domain.Entities.Student>()
            {
                new Domain.Entities.Student()
                {
                    StudentId = Guid.NewGuid(),
                    FirstName = "StudentName1",
                    LastName = "StudentSurname1",
                    Email = "StudentSurname1@gmail.com",
                    Gender = Student.Models.Models.GenderEnum.Female,
                    Phone = "0123456789"
                },

                new Domain.Entities.Student()
                {
                    StudentId = Guid.NewGuid(),
                    FirstName = "StudentName2",
                    LastName = "StudentSurname2",
                    Email = "StudentSurname2@gmail.com",
                    Gender = Student.Models.Models.GenderEnum.Female,
                    Phone = "0123456749"
                },
            };

            _studentRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(students);

            var getAllStudents = new StudentService(_studentRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act

            var result = await Assert.ThrowsAsync<Exception>(() => getAllStudents.GetAllStudentsAsync());

            //Assert

            Assert.Equal("Three or more  items need to be returned", result.Message);
        }

        [Fact]
        public async Task RemoveStudentAsync_Success()
        {
            // Arrange

            var student = new Domain.Entities.Student
            {
                StudentId = Guid.NewGuid(),
                FirstName = "Rose",
                LastName = "Bocibo",
                Email = "rose@gmail.com",
                Phone = "0123456789",
                Gender = Student.Models.Models.GenderEnum.Female
            };

            _studentRepositoryMock.Setup(r => r.FindByExpressionAsyn(It.IsAny<Expression<Func<Domain.Entities.Student, bool>>>())).ReturnsAsync(student);
            _studentRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Expression<Func<Domain.Entities.Student, bool>>>())).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);

            var removeStudent = new StudentService(_studentRepositoryMock.Object, _unitOfWorkMock.Object);

            // Act

            await removeStudent.RemoveStudentAsync(student.StudentId);

            // Assert

            _studentRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Expression<Func<Domain.Entities.Student, bool>>>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveStudentAsync_Fail()
        {
            // Arrange

            var studentId = Guid.NewGuid();

            _studentRepositoryMock.Setup(r => r.FindByExpressionAsyn(It.IsAny<Expression<Func<Domain.Entities.Student, bool>>>())).ReturnsAsync((Domain.Entities.Student)null);

            var removeStudentById = new StudentService(_studentRepositoryMock.Object, _unitOfWorkMock.Object);

            // Act

            var result = await Assert.ThrowsAsync<Exception>(() => removeStudentById.RemoveStudentAsync(studentId));

            //Assert

            Assert.Equal($"The student with {studentId} does not exist.", result.Message);
        }
    }
}