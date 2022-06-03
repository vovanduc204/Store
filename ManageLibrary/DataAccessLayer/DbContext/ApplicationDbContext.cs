using DataAccessLayer.EntityConfigurations;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DbContext
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        DbContextOptions<ApplicationDbContext> options;
        public int CurrentUserProfileId = -1;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
            this.options = options;
        }

        public ApplicationDbContext Create(int timeout)
        {
            var db = new ApplicationDbContext(options);
            db.Database.SetCommandTimeout(timeout);
            return db;
        }

        public DbSet<Author> Author { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());
        }
    }
}
