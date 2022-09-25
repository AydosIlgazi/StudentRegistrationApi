

namespace StudentRegistration.Application.Commands;

public class CreateTermCommandHandler : IRequestHandler<CreateTermCommand, int>
{
    private readonly ITermRepository _termRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public CreateTermCommandHandler(ITermRepository termRepository, IMapper mapper, ILogger logger)
    {
        _termRepository = termRepository;
        _mapper = mapper;
        _logger = logger?.ForContext<CreateTermCommandHandler>() ?? throw new ArgumentNullException(nameof(_logger));

    }
    public async Task<int> Handle(CreateTermCommand request, CancellationToken cancellationToken)
    {
        var termWeeklySlots = _mapper.Map<TermWeeklySlots>(request.TermWeeklySlots);
        Semester semester=null;
        if(request.SemesterType!=null && request.Year != null)
        {
            semester = new Semester() { SemesterType = request.SemesterType.Value, Year = request.Year.Value };

        }

        Term lastTerm= _termRepository.GetLast();

        Term term = TermService.CreateNewTerm(lastTerm, semester, termWeeklySlots);
        _termRepository.Add(term);
        _logger.Information("Creating term {@Term}", term);
        await _termRepository.UnitOfWork.Save();

        return term.Id;
    }

}

