using System;
using System.Collections.Generic;
using System.Text;
using BookManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
// https://www.entityframeworktutorial.net/code-first/configure-many-to-many-relationship-in-code-first.aspx

namespace BookManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Author> Authors  { get; set; }
        public DbSet<Book> Books { get; set; }
 
        public DbSet<Authorship> Authorships { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Loaner> Loaners { get; set; }

        public DbSet<Location> Location { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
          
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Authorship>()
              .HasIndex(a => new {
                  a.AuthorID,
                  a.BookID
              }).IsUnique();
        }

        


}
}
