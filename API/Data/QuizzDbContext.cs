

using API.Models;
using API.Models.Domain;
using API.Models.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Question = API.Models.Domain.Questions.Question;

namespace API.Data


{
    public class QuizzDbContext : DbContext
    {
        public QuizzDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Models.Domain.Questions.Question> Questions { get; set; }

        public DbSet<API.Models.Domain.Questions.Question> Question { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring entity relationships and properties

            // Configuring the Question table to use a discriminator column for TPH (Table-Per-Hierarchy)
            modelBuilder.Entity<Question>()
                .HasDiscriminator<string>("QuestionType")
                .HasValue<Question>("Base")
                .HasValue<Models.Domain.Questions.MultipleChoiceQuestion>("MultipleChoice");

            // Setting up relationships
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasMany(a => a.Questions)
                      .WithOne(q => q.Admin)
                      .HasForeignKey(q => q.AdminId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Answers)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);


        }

    }
}
