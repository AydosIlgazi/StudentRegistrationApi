namespace StudentRegistration.Domain.UnitTests.Aggregates;

public class ConsentTests {

    private const string studentId = "123";
    private const string lectureId = "456";

    [Fact]
    public void create_consent_successfully()
    {
    
        // Act
        Consent consent = new Consent(lectureId, studentId);
    
        //Assert
        Assert.NotNull(consent);
        Assert.Equal(consent.ConsentStatus, ConsentStatus.Waiting);
    }

    [Fact]
    public void accept_consent_successfully()
    {
        // Arrange
        Consent consent = new Consent(lectureId, studentId);

        // Act
        consent.AcceptConsent();
    
        // Assert
        Assert.Equal(consent.ConsentStatus, ConsentStatus.Accepted);
        Assert.Contains(consent.DomainEvents,@event =>
        {
            if(@event is ConsentAcceptedDomainEvent consentAcceptedDomainEvent){
                if(consentAcceptedDomainEvent.LectureId == lectureId)
                {
                    return true;
                }
            }
            return false;
        });
    }

    [Fact]
    public void reject_consent_successfully()
    {
        // Arrange
        Consent consent = new Consent(lectureId, studentId);

        // Act
        consent.RejectConsent();
    
        // Assert
        Assert.Equal(consent.ConsentStatus, ConsentStatus.Rejected);
    }

    [Fact]
    public void rejecting_consent_is_invalid_when_consent_is_already_decided()
    {
        // Arrange
        Consent consent = new Consent(lectureId, studentId);

        consent.RejectConsent();

    
        // Assert
        Assert.Throws<Exception>(()=>consent.RejectConsent());
    }
    [Fact]
    public void accepting_consent_is_invalid_when_consent_is_already_decided()
    {
        // Arrange
        Consent consent = new Consent(lectureId, studentId);

        consent.AcceptConsent();

    
        // Assert
        Assert.Throws<Exception>(()=>consent.AcceptConsent());
    }
}