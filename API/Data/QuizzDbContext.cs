

using API.Models;
using API.Models.Domain;
using API.Models.Domain.Auth;
using API.Models.Domain.Questions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Question = API.Models.Domain.Questions.Question;
using Microsoft.AspNetCore.Identity;
using API.Models.Domain.Extra;
using API.Models.Domain.Reports;

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

            // Test - Report Relationship
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Test)
                .WithOne(t => t.Report)
;

            // Report - QuestionAssessment Relationship
            modelBuilder.Entity<QuestionAssessment>()
                .HasOne(qa => qa.Report)
                .WithMany(r => r.QuestionAssessments)
                .HasForeignKey(qa => qa.ReportId);

            // Question - QuestionAssessment Relationship
            modelBuilder.Entity<QuestionAssessment>()
                .HasOne(qa => qa.Question)
                .WithMany(q => q.QuestionAssessments)
                .HasForeignKey(qa => qa.QuestionId);

        }



        public DbSet<API.Models.Domain.Answer> Answer { get; set; } = default!;

        public DbSet<Framework> Frameworks { get; set; }


        public DbSet<Report> Reports { get; set; }
        public DbSet<QuestionAssessment> QuestionAssessments { get; set; }


    }
}
