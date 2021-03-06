namespace StudentRegistration.Domain.Aggregates;

public class Lecturer :AggregateRoot
{
	private string _lecturerId;
	private  List<Schedule> _schedules;

	public Lecturer(string lecturerId, Schedule schedule)
	{
		_schedules = new List<Schedule>();
		_schedules.Add(schedule);
		_lecturerId = lecturerId;
	}

	public void AddNewScheduleToLecturer(Schedule schedule)
	{
		foreach(Schedule s in _schedules){
			if(s.TermId == schedule.TermId)
			{
				throw new Exception("You cannot add multiple scheduled to the same term");
			}
		}
		var sch=_schedules.Where(s=> s.IsActive == true).FirstOrDefault();
		if(sch != null){
			throw new Exception("Lecturer has active schedule, close it before adding new one");
		}
	}

}