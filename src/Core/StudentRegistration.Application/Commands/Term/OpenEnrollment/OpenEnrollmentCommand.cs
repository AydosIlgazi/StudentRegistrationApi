
namespace StudentRegistration.Application.Commands;

public class OpenEnrollmentCommand : IRequest<bool>
{
    public int TermId { get; init; }
}


