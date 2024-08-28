

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
        public QuizzDbContext(DbContextOptions options) : base(options)
        {

        }

        

        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Models.Domain.Questions.Question> Questions { get; set; }

        public DbSet<Test> Tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Question>()
                .HasDiscriminator<string>("QuestionType")
                .HasValue<Question>("Base")
                .HasValue<Models.Domain.Questions.MultipleChoiceQuestion>("MultipleChoice");

           ;

            modelBuilder.Entity<Question>()
                .HasDiscriminator<string>("QuestionType")
                .HasValue<Question>("Base")
                .HasValue<Models.Domain.Questions.TrueFalseQuestion>("TrueFalse");

            ;



            /*
            modelBuilder.Entity<Answer>()
          .HasOne(a => a.User)
          .WithMany() // Or .WithMany(u => u.Answers) if User entity has collection of answers
          .HasForeignKey(a => a.User)
          .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or ClientSetNull if needed 


            Unable to create a 'DbContext' of type ''.
            The exception 'The property or navigation 'User' cannot be added to the 'Answer' type because a property or navigation with the same name already exists on the 'Answer' type.' was thrown while attempting to create an instance. *
            For the different patterns supported at design time, see https://go.microsoft.com/fwlink/?linkid=851728
            */




        }




        public DbSet<API.Models.Domain.Answer> Answer { get; set; } = default!;
        public DbSet<API.Models.Domain.Test> Test { get; set; } = default!;

        public DbSet<Framework> Frameworks { get; set; }




    }
}
