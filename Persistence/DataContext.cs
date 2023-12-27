using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistence;

public class DataContext : IdentityDbContext<AppUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityAttendee> ActiviteyAttendees { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ActivityAttendee>( x => {
            x.HasKey( aa=> new {aa.AppUserId, aa.ActivityId});
        });

        builder.Entity<ActivityAttendee>( x => {
            x.HasOne(x => x.AppUser)
                .WithMany(x => x.Activiites)
                .HasForeignKey(x => x.AppUserId);
        });
        builder.Entity<ActivityAttendee>( x => {
            x.HasOne(x => x.Activity)
                .WithMany(x => x.Attendees)
                .HasForeignKey(x => x.ActivityId);
        });
    }
}
