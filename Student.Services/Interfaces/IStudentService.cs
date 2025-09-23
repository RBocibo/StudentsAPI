namespace Students.Services.Interfaces
{
    public interface IStudentService
    {
        Task<ICollection<Domain.Entities.Student>> GetAllStudentsAsync();
        Task<Domain.Entities.Student> CreateStudentAsync(Domain.Entities.Student student);
        Task RemoveStudentAsync(Guid studentId);
    }
}
