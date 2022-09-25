
namespace StudentRegistration.Application.Repositories;

public interface ITermRepository: IRepository<Term>
{
    Term GetLast();
}

