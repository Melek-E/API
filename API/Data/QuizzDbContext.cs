

using API.Models;
using API.Models.Domain;
using API.Models.Domain.Authent;
using API.Models.Domain.Questions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Question = API.Models.Domain.Questions.Question;
using Microsoft.AspNetCore.Identity;
using API.Models.Domain.Extra;

namespace API.Data


{

    public class QuizzDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,
        IdentityUserToken<int>>(options)

    { 




        public DbSet<Models.Domain.Questions.Question> Questions { get; set; }

        public DbSet<Test> Tests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Question>()
                 .HasDiscriminator<string>("QuestionType")
                 .HasValue<MultipleChoiceQuestion>("MultipleChoice")
                 .HasValue<TrueFalseQuestion>("TrueFalse")
                            .HasValue<Question>("Base");



            modelBuilder.Entity<ApplicationUser>()
        .HasMany(ur => ur.UserRoles)
        .WithOne(u => u.User)
        .HasForeignKey(ur => ur.UserId)
        .IsRequired();

            modelBuilder.Entity<Test>()
               .HasMany(t => t.Questions)
               .WithMany();

            modelBuilder.Entity<AppRole>()
          .HasMany(ur => ur.UserRoles)
          .WithOne(u => u.Role)
          .HasForeignKey(ur => ur.RoleId)
          .IsRequired();
        }




        public DbSet<API.Models.Domain.Answer> Answer { get; set; } = default!;

        public DbSet<Framework> Frameworks { get; set; }

        public DbSet<ApplicationUser> UsersAAAA {  get; set; }


    }
}
