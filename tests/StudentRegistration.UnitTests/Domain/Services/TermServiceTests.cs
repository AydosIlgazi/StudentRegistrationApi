using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistration.UnitTests.Domain.Services
{
    public class TermServiceTests
    {
        private readonly TermWeeklySlots _termWeeklySlots;
        public TermServiceTests()
        {
            List<DailySlots> dailySlots = new List<DailySlots>();
            dailySlots.Add(new DailySlotsBuilder().Build());
            _termWeeklySlots = new TermWeeklySlots(dailySlots);
        }

        [Fact]
        public void new_term_is_created_without_sending_last_term()
        {
            //Arrange
            Semester newSemester = new Semester()
            {
                SemesterType = SemesterType.Spring,
                Year = 2022
            };

            //Act
            Term newTerm = TermService.CreateNewTerm(null, newSemester, _termWeeklySlots);


            //Assert
            Assert.NotNull(newTerm);
            Assert.Equal(newSemester,newTerm.Semester);
            Assert.Equal(_termWeeklySlots,newTerm.LectureDaysAndSlots);
        }

        [Theory]
        [InlineData(SemesterType.Spring,2022,SemesterType.Summer,2022)]
        [InlineData(SemesterType.Fall,2022,SemesterType.Spring,2023)]
        [InlineData(SemesterType.Summer,2022,SemesterType.Fall,2022)]
        public void new_term_is_created_with_sending_last_term(SemesterType semesterType, int year,SemesterType expectedSemesterType
            ,int expectedYear)
        {
            //Arrange
            Semester lastSemeter = new Semester()
            {
                SemesterType = semesterType,
                Year = year,
            };
            Term lastTerm = new Term(lastSemeter, _termWeeklySlots);
            lastTerm.EndTerm();

            //Act
            Term newTerm = TermService.CreateNewTerm(lastTerm,null, _termWeeklySlots);

            //Assert
            Assert.Equal(expectedSemesterType, newTerm.Semester.SemesterType);
            Assert.Equal(expectedYear, newTerm.Semester.Year);
        }

        [Fact]
        public void new_term_is_created_with_last_term_and_semester_data()
        {
            //Arrange
            Semester lastSemeter = new Semester()
            {
                SemesterType = SemesterType.Fall,
                Year = 2022,
            };
            Term lastTerm = new Term(lastSemeter, _termWeeklySlots);
            lastTerm.EndTerm();
            Semester newSemester = new Semester()
            {
                SemesterType = SemesterType.Spring,
                Year = 2022
            };

            //Act
            Term newTerm = TermService.CreateNewTerm(lastTerm, newSemester, _termWeeklySlots);

            //Assert
            Assert.NotNull(newTerm);
            Assert.Equal(SemesterType.Spring, newTerm.Semester.SemesterType);
            Assert.Equal(2022, newTerm.Semester.Year);
        }

        [Fact]
        public void creating_new_term_is_invalid_when_last_term_is_not_completed()
        {
            //Arrange
            Semester lastSemeter = new Semester()
            {
                SemesterType = SemesterType.Fall,
                Year = 2022,
            };
            Term lastTerm = new Term(lastSemeter, _termWeeklySlots);

            //Act
            Assert.Throws<StudentRegistrationDomainException>(()=> TermService.CreateNewTerm(lastTerm, null, _termWeeklySlots));
        }

        [Fact]
        public void creating_new_term_is_invalid_when_both_last_term_and_new_semester_not_provided()
        {
            //Act
            Assert.Throws<StudentRegistrationDomainException>(() => TermService.CreateNewTerm(null, null, _termWeeklySlots));
        }

    }
}
