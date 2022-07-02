namespace StudentRegistration.Domain.Entities;
public class Person : BaseEntity{
    public string Name {get; private set;}
    public string Surname { get; private set; }
    public string CitizenId {get; private set;}
    public Address Address {get; private set;}
}