
using MediatR;
using StudentRegistration.Application.Behaviors;

namespace StudentRegistration.UnitTests.Application.Commands;

public class CreateTermCommandHandlerTests
{
    private readonly Mock<ITermRepository> _termRepositoryMock;
    private readonly IMapper _mapper;
    private readonly Mock<ILogger> _logger;
    private readonly Mock<ILogger> _validationLogger;

    List<Term> _termList;
    TermWeeklySlotsDTO _termWeeklySlotsDTO;

    public CreateTermCommandHandlerTests()
    {
        _termList = new List<Term>()
        {
            new TermBuilder().WithSemester(SemesterType.Fall,2021).WithId(1).Build(),
            new TermBuilder().WithSemester(SemesterType.Spring,2022).WithId(2).Build(),
            new TermBuilder().WithSemester(SemesterType.Summer,2022).WithId(3).Build(),
        };

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<GeneralMapping>();
        });

        _mapper = mapperConfig.CreateMapper();
        _logger = new Mock<ILogger>();
        _logger.Setup(l => l.ForContext<CreateTermCommandHandler>()).Returns(_logger.Object);
        _validationLogger = new Mock<ILogger>();

        _termRepositoryMock = new Mock<ITermRepository>() ;
        _termRepositoryMock.Setup(t => t.GetAll()).Returns(_termList);
        _termRepositoryMock.Setup(t => t.GetLast()).Returns(_termList.Last());
        _termRepositoryMock.Setup(t=> t.Add(It.IsAny<Term>())).Callback((Term term) => _termList.Add(term));
        _termRepositoryMock.Setup(t => t.UnitOfWork.Save()).Callback(() => _termList.Last().Id = 4);

        TermWeeklySlots termWeeklySlots = new TermWeeklySlots(new List<DailySlots> { new DailySlotsBuilder().Build() });
        _termWeeklySlotsDTO = _mapper.Map<TermWeeklySlotsDTO>(termWeeklySlots);
    }

    [Fact]
    public async void handler_creates_new_term_when_semester_data_provided()
    {
        //Arrange
        _termList.ForEach(term => term.EndTerm());
        CreateTermCommandHandler createTermCommandHandler = new CreateTermCommandHandler(_termRepositoryMock.Object, _mapper, _logger.Object);
        
        //Act
        var termId = await createTermCommandHandler.Handle(new CreateTermCommand()
        {
            SemesterType = SemesterType.Summer,
            Year = 2022,
            TermWeeklySlots= _termWeeklySlotsDTO
        },CancellationToken.None);
        
        //Assert
        var terms = _termRepositoryMock.Object.GetAll();
        Assert.Equal(4, terms.Last().Id);
        Assert.Equal(4, termId);
    }

    [Fact]
    public async void handler_creates_new_term_without_semester()
    {
        //Arrange
        _termList.ForEach(term => term.EndTerm());
         CreateTermCommandHandler createTermCommandHandler = new CreateTermCommandHandler(_termRepositoryMock.Object, _mapper, _logger.Object);

        //Act
        var termId = await createTermCommandHandler.Handle(new CreateTermCommand()
        { TermWeeklySlots = _termWeeklySlotsDTO}, CancellationToken.None);

        //Assert
        var terms = _termRepositoryMock.Object.GetAll();
        Assert.Equal(SemesterType.Fall, terms.Last().Semester.SemesterType);
        Assert.Equal(2022, terms.Last().Semester.Year);
        Assert.Equal(4, termId);

    }

    [Fact]
    public async void handler_validator_returns_create_term_handle_when_request_validated()
    {
        //Arrange
        _termList.ForEach(term => term.EndTerm());
        Mock<IRequestHandler<CreateTermCommand, int>> createTermCommandHandler = new Mock<IRequestHandler<CreateTermCommand, int>>();
        createTermCommandHandler.Setup(ch => ch.Handle(It.IsAny<CreateTermCommand>(), It.IsAny<CancellationToken>()).Result).Returns(5);
        CreateTermCommand command = new CreateTermCommand() { TermWeeklySlots = _termWeeklySlotsDTO};

        //Act
        ValidatonBehavior<CreateTermCommand, int> validator = new ValidatonBehavior<CreateTermCommand, int>(new List<CreateTermCommandValidator>() { new CreateTermCommandValidator() }, _validationLogger.Object);
        var termId = await  validator.Handle(command, CancellationToken.None, () => createTermCommandHandler.Object.Handle(command, CancellationToken.None));

        //Assert
        Assert.Equal(5, termId);
    }

    [Fact]
    public async void handler_validator_returns_error_when_termweeklslots_is_null()
    {
        //Arrange
        CreateTermCommandHandler createTermCommandHandler = new CreateTermCommandHandler(_termRepositoryMock.Object, _mapper, _logger.Object);
        CreateTermCommand command = new CreateTermCommand();

        //Act
        ValidatonBehavior<CreateTermCommand, int> validator = new ValidatonBehavior<CreateTermCommand, int>(new List<CreateTermCommandValidator>() { new CreateTermCommandValidator()}, _validationLogger.Object);
        Exception exc = await Record.ExceptionAsync(()=> validator.Handle(command, CancellationToken.None, () => createTermCommandHandler.Handle(command,CancellationToken.None)));

        //Assert
        Assert.NotNull(exc);
        Assert.IsType<ValidationException>(exc);
        ValidationException validationException = exc as ValidationException;
        Assert.Single(validationException.Errors);
        Assert.Equal(typeof(TermWeeklySlots).Name, validationException.Errors.Single().PropertyName);
    }

    [Fact]
    public async void handler_validator_returns_error_when_dailyslots_is_null()
    {
        //Arrange
        CreateTermCommandHandler createTermCommandHandler = new CreateTermCommandHandler(_termRepositoryMock.Object, _mapper, _logger.Object);
        CreateTermCommand command = new CreateTermCommand() { TermWeeklySlots = new TermWeeklySlotsDTO()};

        //Act
        ValidatonBehavior<CreateTermCommand, int> validator = new ValidatonBehavior<CreateTermCommand, int>(new List<CreateTermCommandValidator>() { new CreateTermCommandValidator() }, _validationLogger.Object);
        Exception exc = await Record.ExceptionAsync(() => validator.Handle(command, CancellationToken.None, () => createTermCommandHandler.Handle(command, CancellationToken.None)));

        //Assert
        Assert.NotNull(exc);
        Assert.IsType<ValidationException>(exc);
        ValidationException validationException = exc as ValidationException;
        Assert.Equal(2, validationException.Errors.Count());
        foreach (var error in validationException.Errors)
        {
            Assert.Contains(typeof(DailySlots).Name, error.PropertyName);
        }
    }


    [Fact]
    public async void handler_validator_returns_error_when_termweeklslots_is_empty()
    {
        //Arrange
        CreateTermCommandHandler createTermCommandHandler = new CreateTermCommandHandler(_termRepositoryMock.Object, _mapper, _logger.Object);
        CreateTermCommand command = new CreateTermCommand() { TermWeeklySlots = new TermWeeklySlotsDTO() { DailySlots = new List<DailySlotsDTO>() } };

        //Act
        ValidatonBehavior<CreateTermCommand, int> validator = new ValidatonBehavior<CreateTermCommand, int>(new List<CreateTermCommandValidator>() { new CreateTermCommandValidator() }, _validationLogger.Object);
        Exception exc = await Record.ExceptionAsync(() => validator.Handle(command, CancellationToken.None, () => createTermCommandHandler.Handle(command, CancellationToken.None)));

        //Assert
        Assert.NotNull(exc);
        Assert.IsType<ValidationException>(exc);
        ValidationException validationException = exc as ValidationException;
        Assert.Single(validationException.Errors);
        Assert.Contains(typeof(DailySlots).Name, validationException.Errors.Single().PropertyName);
    }

}
