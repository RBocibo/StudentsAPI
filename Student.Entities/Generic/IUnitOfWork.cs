namespace Students.Domain.Generic
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
