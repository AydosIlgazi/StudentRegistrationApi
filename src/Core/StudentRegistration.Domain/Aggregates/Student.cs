namespace StudentRegistration.Domain.Aggregates;

public class Student  : AggregateRoot
{
	private string _studentId;
	private List<Schedule> _schedules;

	private List<Enrollment> _enrollments;


	public void EnrollToLecture(LectureVO lecture, Enrollment enrollment)
	{
		var _enrollment = _enrollments.Where(e=> e.LectureId == lecture.LectureId && e.EnrollmentStatus != EnrollmentStatus.Left).FirstOrDefault();
		if(_enrollment != null)
		{
			throw new Exception ("You have active or completed enrollment to this lecture");
		}
		//check lecture is exist in course plan

		//check student schedule available
		var activeSchedule = _schedules.Where(s=> s.IsActive == true).FirstOrDefault();
		if(activeSchedule == null){
			throw new Exception("You cannot enroll classes in this term");
		}
		foreach(Classroom c in lecture.LectureClassSlots){
			foreach(DaySlot ds in c.ClassSlots){
				if(activeSchedule.IsScheduleAvailable(ds.Slot,ds.Day)== false)
				{
					throw new Exception ("your schedule is not available for this lecture");
				}
				
			}
			activeSchedule.FillTheSchedule(c.ClassSlots);
		}

		//check student records(min credit and prerequisites)

		//check distance between consecutive slots

		_enrollments.Add(enrollment);
		AddDomainEvent(new StudentEnrollmentCompletedDomainEvent{
			StudentId = _studentId,
			LectureId = lecture.LectureId
		});

	}
	public void RemoveEnrollment(string lectureId)
	{
		var enrollment = _enrollments.Where(e=>e.LectureId == lectureId).FirstOrDefault();
		if(enrollment == null)
		{
			throw new Exception("You are not enrolled this lecture");
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