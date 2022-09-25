namespace StudentRegistration.Application.Commands;

public class StartTermCommandHandler : IRequestHandler<StartTermCommand, bool>
{
    private readonly ITermRepository _termRepository;
    public StartTermCommandHandler(ITermRepository termRepository)
    {
        _termRepository = termRepository;
    }
    public async Task<bool> Handle(StartTermCommand request, CancellationToken cancellationToken)
    {
        Term term = _termRepository.GetById(request.TermId);

        if (term == null)
            return false;

        term.StartTerm();
        return await _termRepository.UnitOfWork.Save();

    }
}
