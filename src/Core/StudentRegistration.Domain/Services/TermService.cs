namespace StudentRegistration.Domain.Services;

public class TermService 
{
	//Todo unit test domain
	//Todo unit test application
	public static Term CreateNewTerm(Term? lastTerm, Semester? newSemester, TermWeeklySlots termWeeklySlots){
		if(lastTerm!=null && lastTerm.Status != TermStatus.Completed)
        {
			throw new StudentRegistrationDomainException("New term cannot be started if there is an active term");
        }
		if(newSemester==null)
        {
			if (lastTerm == null)
				throw new StudentRegistrationDomainException("First term must be started with providing new semester");

			newSemester = FindNextSemester(lastTerm.Semester);
        }

		Term term = new Term(newSemester, termWeeklySlots);

		
		return term;
	}

	private static Semester FindNextSemester(Semester lastSemester){
		SemesterType semesterType=default;
		int year=lastSemester.Year;

		if(lastSemester.SemesterType==SemesterType.Fall)
        {
			semesterType = SemesterType.Spring;
			year += 1;
        }
		if (lastSemester.SemesterType == SemesterType.Spring)
		{
			semesterType = SemesterType.Summer;
		}
		if (lastSemester.SemesterType == SemesterType.Summer)
		{
			semesterType = SemesterType.Fall;
		}
		return new Semester()
		{ SemesterType = semesterType, Year = year };

	}
}