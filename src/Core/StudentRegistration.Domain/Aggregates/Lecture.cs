namespace StudentRegistration.Domain.Aggregates;

public class Lecture :AggregateRoot
{
	private string? _lecturerId;
	private int _termId;
	private List<Section> _lectureSections;
	private string _courseId;
	private int _capacity;
	private int _totalMinutesWeekly;

	public List<Section> LectureSections => _lectureSections;

	public string LecturerId => _lecturerId;

	public Lecture(TermVO term, CourseDTO course,int capacity)
	{
		if(term.TermStatus != TermStatus.Active)
		{
			throw new StudentRegistrationDomainException("Lecture can be opened in only active terms");
		}

		_termId = term.TermId;
		_courseId = course.CourseId;
		_capacity = capacity;
		_totalMinutesWeekly = course.TotalMinutesWeekly;
		_lectureSections = new List<Section>();
	}

	public int GetTotalLectureDuration()
	{
		int totalMinutes = 0;
		foreach(Section s in _lectureSections){
			foreach(DaySlot ds in s.SectionSlots)
			{
				totalMinutes += ds.Slot.CalculateSlotDuration();

			}
		}
		return totalMinutes;
	}

	public void AddSectionToLecture( Section section, ClassroomDTO classroom)
	{
		int additionalDuration =0;
		foreach(DaySlot ds in section.SectionSlots)
		{
			additionalDuration += ds.Slot.CalculateSlotDuration();
		}
		if(GetTotalLectureDuration()+ additionalDuration > _totalMinutesWeekly){
			throw new StudentRegistrationDomainException("Lecture duration cannot exceedde defined course duration");
		}
		if(classroom.Capacity<_capacity)
		{
			throw new StudentRegistrationDomainException("Lecture Capacity can not exceed the one of the classrooms capacity");
		}
		_lectureSections.Add(section);
		AddDomainEvent(new SectionAddedToLectureDomainEvent{
			DaySlots = section.SectionSlots
		});
	}
	
	public void AssignLecturerToLecture(LecturerVO lecturer)
	{
		if(GetTotalLectureDuration() != _totalMinutesWeekly){
			throw new StudentRegistrationDomainException("Complete all slots before assigning the lecturer ");
		}
		List<DaySlot> daySlots = new List<DaySlot>();
		foreach(Section s in _lectureSections){
			foreach(DaySlot ds in s.SectionSlots){
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