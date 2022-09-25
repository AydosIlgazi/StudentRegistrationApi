
using Microsoft.EntityFrameworkCore;

namespace StudentRegistration.Infrastructure;

public class StudentRegistrationContext : DbContext, IUnitOfWork
{
    public DbSet<Term> Terms { get; set; }

    public StudentRegistrationContext(DbContextOptions<StudentRegistrationContext> options ) : base(options)
    { 
        //migrations
        //dotnet ef migrations add InitialCreate --project .\src\Infrastructure\StudentRegistration.Infrastructure --startup-project .\src\WebApi\StudentRegistration.WebApi
        //dotnet ef database update --project .\src\Infrastructure\StudentRegistration.Infrastructure --startup-project .\src\WebApi\StudentRegistration.WebApi
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<Term>(
           b =>
           {
               b.Property(t=>t.Status);
               b.Property(t => t.IsEnrollmentActive);
               b.OwnsOne(t=>t.Semester,s=>
               {
                   s.Property(p => p.SemesterType).HasColumnName(nameof(Semester.SemesterType));
                   s.Property(p => p.Year).HasColumnName(nameof(Semester.Year));
               });
               b.OwnsOne(t => t.LectureDaysAndSlots, tws =>
                  {
                      tws.OwnsMany(tws => tws.DailySlots, ds =>
                       {
                           ds.Property<int>("Id");
                           ds.HasKey("Id");
                           ds.Property(ds => ds.Day);
                           ds.OwnsMany(ds => ds.Slots, s=>
                           {
                               s.Property<int>("Id");
                               s.HasKey("Id");
                               s.OwnsOne(s => s.StartTime, p=>
                               {
                                   p.Property(p => p.Hour).HasColumnName("StartHour");
                                   p.Property(p => p.Miniute).HasColumnName("StartMinute");

                               });
                               s.OwnsOne(s => s.EndTime, p=>
                               {
                                   p.Property(p=>p.Hour).HasColumnName("EndHour"); ;
                                   p.Property(p=>p.Miniute).HasColumnName("EndMinute");
                               });
                           }
                           );
                       });
                      
                  });
           });
    }

    public async Task<bool> Save()
    {
         await base.SaveChangesAsync();
        return true;
    }
}
