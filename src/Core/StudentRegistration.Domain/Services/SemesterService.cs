namespace StudentRegistration.Domain.Services;

public class SemesterService 
{
	//Todo Fix Exceptions
	public static Semester StartNewSemester(Semester latestSemester, Semester newSemester){
		var nextSemesterType = FindNextSemesterType(latestSemester.SemesterType);
		if(nextSemesterType != newSemester.SemesterType) throw new Exception("Invalid semester");
		if(newSemester.Year < latestSemester.Year) throw new Exception ("Invalid");
		newSemester.StartSemester();
		return newSemester;
	}

	private static SemesterType FindNextSemesterType(SemesterType semesterType){
		if(semesterType == SemesterType.Fall) return SemesterType.Spring;
		if(semesterType == SemesterType.Spring) return SemesterType.Summer;
		if(semesterType == SemesterType.Summer) return SemesterType.Fall;
		throw new Exception("Undefined Semester Type");
	}
}