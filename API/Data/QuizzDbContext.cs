

using API.Models;
using API.Models.Domain;
using API.Models.Domain.Auth;
using API.Models.Domain.Questions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Question = API.Models.Domain.Questions.Question;
using Microsoft.AspNetCore.Identity;
using API.Models.Domain.Extra;

namespace API.Data


{
    public class QuizzDbContext : IdentityDbContext<ApplicationUser>
    {
        public QuizzDbContext(DbContextOptions<QuizzDbContext> options) : base(options)
        {

        }


        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        public DbSet<Models.Domain.Questions.Question> Questions { get; set; }

        public DbSet<Test> Tests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Question>()
                 .HasDiscriminator<string>("QuestionType")
                 .HasValue<MultipleChoiceQuestion>("MultipleChoice")
                 .HasValue<TrueFalseQuestion>("TrueFalse")
                            .HasValue<Question>("Basic");


            

            modelBuilder.Entity<Test>()
               .HasMany(t => t.Questions)
               .WithMany();

            modelBuilder.Entity<Question>()
           .HasMany(q => q.Answers)
           .WithOne(a => a.Question)
           .HasForeignKey(a => a.QuestionId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Frameworks)
                .WithMany();



            // Configure the Report entity
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Test)  // Link to Test
                .WithMany()  // A test can have many reports
                .HasForeignKey(r => r.TestId);  // Foreign key in Report

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Question)  // Link to Question
                .WithMany()  // A question can have many reports
                .HasForeignKey(r => r.QuestionId);  // Foreign key in Report

        }



        public DbSet<API.Models.Domain.Answer> Answer { get; set; } = default!;

        public DbSet<Framework> Frameworks { get; set; }

        public DbSet<Report> Reports { get; set; }  


    }
}
