namespace StudentRegistration.Application.Repositories;

public interface IRepository<T> where T : AggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
    T GetById(int id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}

public interface IUnitOfWork : IDisposable
{
    Task<bool> Save();
}