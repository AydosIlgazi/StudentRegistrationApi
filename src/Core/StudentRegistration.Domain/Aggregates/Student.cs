namespace StudentRegistration.Domain.Aggregates;

public class Student  : AggregateRoot
{
	private string _studentId;
	private List<Schedule> _schedules;

	private List<Enrollment> _enrollments;
	
	public IReadOnlyCollection<Enrollment> Enrollments => _enrollments;
	public IReadOnlyCollection<Schedule> Schedules => _schedules;

	public void EnrollToLecture(LectureVO lecture, Enrollment enrollment)
	{
		var _enrollment = _enrollments.FirstOrDefault(e=> e.LectureId == lecture.LectureId && e.EnrollmentStatus != EnrollmentStatus.Left);
		if(_enrollment != null)
		{
			throw new StudentRegistrationDomainException ("You have active or completed enrollment to this lecture");
		}
		//check lecture is exist in course plan

		//check student schedule available
		var activeSchedule = _schedules.FirstOrDefault(s=> s.IsActive == true);
		if(activeSchedule == null){
			throw new StudentRegistrationDomainException("You cannot enroll classes in this term");
		}
		foreach(Section s in lecture.LectureSections){
			foreach(DaySlot ds in s.SectionSlots){
				if(activeSchedule.IsScheduleAvailable(ds.Slot,ds.Day)== false)
				{
					throw new StudentRegistrationDomainException ("your schedule is not available for this lecture");
				}
				
			}
			activeSchedule.FillTheSchedule(s.SectionSlots);
		}

		//check student records(min credit and prerequisites)

		//check distance between consecutive slots

		_enrollments.Add(enrollment);
		AddDomainEvent(new StudentEnrollmentCreatedDomainEvent{
			StudentId = _studentId,
			LectureId = lecture.LectureId
		});

	}
	public void RemoveEnrollment(int lectureId)
	{
		var enrollment = _enrollments.FirstOrDefault(e=>e.LectureId == lectureId);
		if(enrollment == null)
		{
			throw new StudentRegistrationDomainException("You are not enrolled this lecture");
		}
		enrollment.CancelEnrollment();
		AddDomainEvent(new StudentEnrollmentCanceledDomainEvent{
			StudentId = _studentId,
			LectureId= lectureId
		});
	}
	public Student( string studenId, Schedule schedule )
	{
		_schedules = new List<Schedule>();
		_schedules.Add(schedule);
		_enrollments = new List<Enrollment>();
		_studentId = studenId;
	}

}