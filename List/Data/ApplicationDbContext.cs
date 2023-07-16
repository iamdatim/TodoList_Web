using List.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace List.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Todo> ToDoList { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

       
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<ApplicationUser>(entity =>
        //    {
        //        entity.ToTable("ApplicationUser");
  
        //    });


        //    modelBuilder.Entity<ApplicationUser>()
        //        .HasMany(u => u.Todos)
        //        .WithOne()
        //        .HasConstraintName("FK_ApplicationUser");





        //    modelBuilder.Entity<Todo>(entity =>
        //    {
        //        entity.HasKey(t => t.Id);
        //        entity.ToTable("Todo");

        //        entity.HasOne(t => t.User)
        //        .WithMany()
        //        .HasForeignKey(t => t.UserId)
        //        .HasPrincipalKey(u => u.Id)
        //        .HasConstraintName("FK_Todo");

        //    });

          
        //}




    }

}
