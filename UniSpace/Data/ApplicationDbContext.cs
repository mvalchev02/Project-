using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniSpace.Data.Models;

namespace UniSpace.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Proffesseur> Professeurs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasOne(r => r.Room)
                       .WithMany()
                       .HasForeignKey(r => r.RoomId);

                entity.HasOne(r => r.User)
                       .WithMany()
                       .HasForeignKey(r => r.UserId);

                entity.HasOne(r => r.Specialty)
                   .WithMany()
                   .HasForeignKey(r => r.SpecialtyId);

                entity.HasOne(r => r.Subject)
                   .WithMany()
                   .HasForeignKey(r => r.SubjectId);
            });
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasOne(s => s.Specialty)
                      .WithMany()
                      .HasForeignKey(s => s.SpecialtyId);
            });
            modelBuilder.Entity<Proffesseur>(entity =>
            {
                entity.HasMany(p => p.TaughtSubjects)
                      .WithMany()
                      .UsingEntity(join => join.ToTable("ProfessorSubjects"));
            });



        }
    }
}
