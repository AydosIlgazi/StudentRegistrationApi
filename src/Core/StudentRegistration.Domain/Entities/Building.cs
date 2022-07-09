namespace StudentRegistration.Domain.Entities;

public class Building : BaseEntity 
{
	private double _latitude;
	private double _longitude;
	private string _name;
	public Building(double latitude, double longitude, string name)
	{
		_latitude = latitude;
		_longitude = longitude;
		_name = name;
	}
}