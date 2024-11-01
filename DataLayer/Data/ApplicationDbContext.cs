using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Contacts)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Incident>()
                .HasMany(i => i.Accounts)
                .WithOne(a => a.Incident)
                .HasForeignKey(a => a.Incident_Name);

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Name)
                .IsUnique();
        }
    }
}
