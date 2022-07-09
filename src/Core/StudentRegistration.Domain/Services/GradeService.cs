namespace StudentRegistration.Domain.Services;

public static class GradeService
{
	public static double ConvertGradeFromLatterToValue(Grade grade)
	{
		switch(grade)
		{
			case Grade.DD:
				return 1.0;
			case Grade.DC:
				return 1.5;
			case Grade.CC:
				return 2.0;
			case Grade.CB:
				return 2.5;
			case Grade.BB:
				return 3.0;
			case Grade.BA:
				return 3.5;
			case Grade.AA:
				return 4.0;
			default :
				return 0;
		
		}
	}
} 