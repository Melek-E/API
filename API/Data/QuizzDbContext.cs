

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
                      .OnDelete(DeleteBehavior.NoAction);
            });

                


                

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





    }
}
