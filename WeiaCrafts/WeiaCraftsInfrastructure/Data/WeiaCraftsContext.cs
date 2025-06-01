using Microsoft.EntityFrameworkCore;
using WeiaCraftsDomain.Entities;

namespace WeiaCraftsInfrastructure.Data
{
    public class WeiaCraftsContext : DbContext
    {
        public WeiaCraftsContext(DbContextOptions<WeiaCraftsContext> options) : base(options) { }

        public DbSet<AccountStatus> AccountStatuses { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<CourseFeedback> CourseFeedbacks { get; set; } = null!;
        public DbSet<CourseMaterial> CourseMaterials { get; set; } = null!;
        public DbSet<CourseRating> CourseRatings { get; set; } = null!;
        public DbSet<Enrollment> Enrollments { get; set; } = null!;
        public DbSet<Feedback> Feedbacks { get; set; } = null!;
        public DbSet<Lesson> Lessons { get; set; } = null!;
        public DbSet<Material> Materials { get; set; } = null!;
        public DbSet<PlatformFeedback> PlatformFeedbacks { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Subscription> Subscriptions { get; set; } = null!;
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; } = null!;
        public DbSet<Trainee> Trainees { get; set; } = null!;
        public DbSet<Trainer> Trainers { get; set; } = null!;
        public DbSet<TrainerAlert> TrainerAlerts { get; set; } = null!;
        public DbSet<TrainerFeedback> TrainerFeedbacks { get; set; } = null!;
        public DbSet<TrainerRating> TrainerRatings { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<Video> Videos { get; set; } = null!;
        public DbSet<VideoFeedback> VideoFeedbacks { get; set; } = null!;
        public DbSet<VideoRating> VideoRatings { get; set; } = null!;
        public DbSet<PaidAccountTrainee> PaidAccountTrainees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccountStatus>()
                .Property(a => a.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Course>().OwnsOne(c => c.Price, p =>
            {
                p.Property(a => a.Amount).HasPrecision(18, 2);
            });
            modelBuilder.Entity<Cart>().OwnsOne(c => c.TotalAmount, amt =>
            {
                amt.Property(a => a.Amount).HasPrecision(18, 2);
            });
            modelBuilder.Entity<CartItem>().OwnsOne(ci => ci.Price, price =>
            {
                price.Property(p => p.Amount).HasPrecision(18, 2);
            });
            modelBuilder.Entity<Subscription>().OwnsOne(s => s.Price, price =>
            {
                price.Property(p => p.Amount).HasPrecision(18, 2);
            });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Cart>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithOne(u => u.Cart)  
                .HasForeignKey<Cart>(c => c.UserName)
                .HasPrincipalKey<User>(u => u.UserName);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => ur.Id);

            modelBuilder.Entity<UserRole>()
                .HasIndex(ur => new { ur.UserName, ur.RoleId })
                .IsUnique();

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserName)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Trainer)
                .WithMany(t => t.CreatedCourses)
                .HasForeignKey(c => c.TrainerUserName)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Lessons)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lesson>()
                .Property(l => l.SequenceOrder)
                .IsRequired();

            modelBuilder.Entity<Lesson>()
                .HasIndex(l => new { l.CourseId, l.SequenceOrder })
                .IsUnique();

            modelBuilder.Entity<Video>()
                .HasOne(v => v.Course)
                .WithMany()
                .HasForeignKey(v => v.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseMaterial>()
                .HasKey(cm => new { cm.CourseId, cm.MaterialId });

            modelBuilder.Entity<CourseMaterial>()
                .HasOne(cm => cm.Course)
                .WithMany(c => c.CourseMaterials)
                .HasForeignKey(cm => cm.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CourseMaterial>()
                .HasOne(cm => cm.Material)
                .WithMany(m => m.CourseMaterials)
                .HasForeignKey(cm => cm.MaterialId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Course)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.LastAccessedLesson)
                .WithMany(l => l.Enrollments)
                .HasForeignKey(e => e.LastAccessedLessonId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.EnrolledTrainees)
                .WithMany(t => t.EnrolledCourses);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.CompletedTrainees)
                .WithMany(t => t.CompletedCourses)
                .UsingEntity(j => j.ToTable("TraineeCompletedCourses"));

            modelBuilder.Entity<Course>()
                .HasMany(c => c.FavoritedByTrainees)
                .WithMany(t => t.FavoriteCourses)
                .UsingEntity(j => j.ToTable("TraineeFavoriteCourses"));

            modelBuilder.Entity<TrainerAlert>()
                .HasOne(ta => ta.Trainer)
                .WithMany(t => t.Alerts)
                .HasForeignKey(ta => ta.TrainerUserName)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PaidAccountTrainee>()
                .HasMany(p => p.Subscriptions)
                .WithOne(s => s.PaidAccountTrainee)
                .HasForeignKey(s => s.PaidAccountTraineeUserName)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PaidAccountTrainee>()
                .HasOne(p => p.CurrentSubscription)
                .WithMany()
                .HasForeignKey(p => p.CurrentSubscriptionId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Feedback>()
                .HasDiscriminator<string>("FeedbackType")
                .HasValue<TrainerFeedback>("Trainer")
                .HasValue<VideoFeedback>("Video")
                .HasValue<CourseFeedback>("Course")
                .HasValue<PlatformFeedback>("Platform");

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Trainee)
                .WithMany(t => t.Feedbacks)
                .HasForeignKey(f => f.TraineeUserName)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TrainerFeedback>()
                .HasOne(tf => tf.Trainer)
                .WithMany(t => t.FeedbacksReceived)
                .HasForeignKey(tf => tf.TrainerUserName)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VideoFeedback>()
                .HasOne(vf => vf.Video)
                .WithMany(v => v.FeedbacksRecieved)
                .HasForeignKey(vf => vf.VideoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CourseFeedback>()
                .HasOne(cf => cf.Course)
                .WithMany(c => c.FeedbacksRecieved)
                .HasForeignKey(cf => cf.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rating>()
                .HasDiscriminator<string>("RatingType")
                .HasValue<TrainerRating>("Trainer")
                .HasValue<VideoRating>("Video")
                .HasValue<CourseRating>("Course");

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Trainee)
                .WithMany(t => t.Ratings)
                .HasForeignKey(r => r.TraineeUserName)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TrainerRating>()
                .HasOne(tr => tr.Trainer)
                .WithMany(t => t.RatingsReceived)
                .HasForeignKey(tr => tr.TrainerUserName)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VideoRating>()
                .HasOne(vr => vr.Video)
                .WithMany(v => v.RatingsReceived)
                .HasForeignKey(vr => vr.VideoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CourseRating>()
                .HasOne(cr => cr.Course)
                .WithMany(c => c.RatingsReceived)
                .HasForeignKey(cr => cr.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.SubscriptionType)
                .WithMany()
                .HasForeignKey(s => s.SubscriptionTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}