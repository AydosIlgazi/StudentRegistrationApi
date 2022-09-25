
namespace StudentRegistration.Application.Commands;

public class StartTermCommand : IRequest<bool>
{
    public int TermId { get; init; }
}
