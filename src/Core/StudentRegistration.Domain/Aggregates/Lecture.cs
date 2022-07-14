namespace StudentRegistration.Domain.Aggregates;

public class Lecture :AggregateRoot
{
	private string? _lecturerId;
	private string _termId;
	private List<Classroom> _lectureClassSlots;
	private string _courseId;
	private int _capacity;

	public Lecture(TermVO term, List<Classroom> lectureClassSlots, List<ClassroomDTO> classrooms, CourseDTO course,int capacity)
	{
		if(term.TermStatus != TermStatus.Active)
		{
			throw new Exception("Lecture can be opened in only active terms");
		}

		if(GetTotalLectureDuration()>course.TotalMinutesWeekly){
			throw new Exception("Lecture duration cannot exceed the total course duration");
		}

		int maxCapacity = int.MaxValue;
		foreach(ClassroomDTO classroom in classrooms)
		{
			if(classroom.Capacity < maxCapacity)
				maxCapacity = classroom.Capacity;
		}
		if(capacity > maxCapacity)
		{
			throw new Exception ("Lecture Capacity can not exceed the one of the classrooms capacity");
		}
		_termId = term.TermId;
		_lectureClassSlots = lectureClassSlots;
		_courseId = course.CourseId;
		_capacity = capacity;
	}

	public int GetTotalLectureDuration()
	{
		int totalMinutes = 0;
		foreach(Classroom c in _lectureClassSlots){
			foreach(DaySlot ds in c.ClassSlots)
			{
				totalMinutes += ds.Slot.CalculateSlotDuration();

			}
		}
		return totalMinutes;
	}

	public void AddSectionToLecture(DaySlot daySlot, Classroom lectureClassSlot, ClassroomDTO classroom,CourseDTO course)
	{
		int additionalDuration = daySlot.Slot.CalculateSlotDuration();
		if(GetTotalLectureDuration()+ additionalDuration > course.TotalMinutesWeekly){
			throw new Exception("Lecture duration cannot exceedde defined course duration");
		}
		if(classroom.Capacity<_capacity)
		{
			throw new Exception ("Lecture Capacity can not exceed the one of the classrooms capacity");
		}
		_lectureClassSlots.Add(lectureClassSlot);
	}
	
	public void AssignLecturerToLecture(LecturerVO lecturer, CourseDTO course)
	{
		if(GetTotalLectureDuration() != course.TotalMinutesWeekly){
			throw new Exception("Complete all slots before assigning the lecturer ");
		}
		List<DaySlot> daySlots = new List<DaySlot>();
		foreach(Classroom c in _lectureClassSlots){
			foreach(DaySlot ds in c.ClassSlots){
				if(lecturer.Schedule.IsScheduleAvailable(ds.Slot,ds.Day)== false)
				{
					throw new Exception ("Lecturer is not available during slot");
				}
				daySlots.Add(ds);
			}
		}
		_lecturerId = lecturer.LecturerId;
		AddDomainEvent(new LecturerAssignedToLectureDomainEvent{
			LecturerId = _lecturerId,
			DaySlots = daySlots
		});
	}
}