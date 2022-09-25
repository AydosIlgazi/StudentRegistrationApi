
namespace StudentRegistration.Infrastructure.Repositories;

public class TermRepository : BaseRepository<Term>, ITermRepository
{
    public TermRepository(StudentRegistrationContext context) : base(context)
    {
    }

    public Term GetLast()
    {
        return null;
    }

}
