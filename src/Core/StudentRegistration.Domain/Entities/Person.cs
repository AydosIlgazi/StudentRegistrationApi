namespace StudentRegistration.Domain.Entities;
public class Person : BaseEntity{

    private string _citizenId;
    private FullName _fullName;
    private Address _address;
    private DateTime _birthDate;
    private string _phoneNumber;
    private string _personalMailAddress;

    public Person(string citizenId, FullName fullName, Address address, DateTime birthDate, string phoneNumber, string personalMailAddress)
    {
        _citizenId = citizenId;
        _fullName  = fullName;
        _address = address;
        _birthDate = birthDate;
        _phoneNumber = phoneNumber;
        _personalMailAddress= personalMailAddress;
    }

    protected void UpdatePersonnelInfo(string? citizenId=null, FullName? fullName=null, Address? address=null, DateTime? birthDate=null, string? phoneNumber=null, string? personalMailAddress=null){
        _citizenId = string.IsNullOrEmpty(citizenId) ? _citizenId : citizenId;
        _fullName = fullName is null ? _fullName : fullName;
        _address = address is null ? _address : address;
        _birthDate = birthDate is null ? _birthDate : birthDate.Value;
        _phoneNumber = string.IsNullOrEmpty(phoneNumber) ? _phoneNumber : phoneNumber;
        _personalMailAddress = string.IsNullOrEmpty(personalMailAddress) ? _personalMailAddress : personalMailAddress;
    }
    public string CitizenId => _citizenId;
    public  FullName FullName => _fullName;
    public Address Address => _address;
    public DateTime BirthDate => _birthDate;
    public string PhoneNumber => _phoneNumber;
    public string MailAddress => _personalMailAddress;


}