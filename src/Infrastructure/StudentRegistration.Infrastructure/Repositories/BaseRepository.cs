namespace StudentRegistration.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where  TEntity: AggregateRoot
{
    private readonly StudentRegistrationContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public BaseRepository(StudentRegistrationContext context)
    {
        _context = context;
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TEntity> GetAll()
    {
        throw new NotImplementedException();
    }

    public TEntity GetById(int id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public void Update(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
