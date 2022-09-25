namespace StudentRegistration.Application.Commands;

public class OpenEnrollmentCommandHandler : IRequestHandler<OpenEnrollmentCommand, bool>
{
    private readonly ITermRepository _termRepository;
    public OpenEnrollmentCommandHandler(ITermRepository termRepository)
    {
        _termRepository = termRepository;
    }
    public async Task<bool> Handle(OpenEnrollmentCommand request, CancellationToken cancellationToken)
    {
        Term term = _termRepository.GetById(request.TermId);

        if (term == null)
            return false;

        term.OpenEnrollment();
        return await _termRepository.UnitOfWork.Save();

    }
}

