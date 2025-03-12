using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MovieRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .ToTable("Employees");
            modelBuilder.Entity<Actor>()
                .ToTable("Actors");
            modelBuilder.Entity<Client>()
                .ToTable("Clients");

            modelBuilder.Entity<Actor>()
                .HasMany(a => a.Movies)
                .WithMany(m => m.Actors);

            modelBuilder.Entity<Rent>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Rents)
                .HasForeignKey(r => r.ClientId);

            modelBuilder.Entity<Rent>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Rents)
                .HasForeignKey(r => r.MovieId);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Reviews)
                .WithOne(r => r.Movie)
                .HasForeignKey(r => r.MovieId);
        }
    }
}
