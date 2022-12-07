using CinemaMvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Actors_Movies>().HasKey(
            am => new
                {
                    am.ActorId,
                    am.MovieId
    });
            modelBuilder.Entity<Actors_Movies>()
                .HasOne(m => m.Movie).WithMany(am => am.Actors_Movies)
                .HasForeignKey(m => m.MovieId);
    modelBuilder.Entity<Actors_Movies>()
               .HasOne(m => m.Actor).WithMany(am => am.Actors_Movies)
               .HasForeignKey(m => m.ActorId);
}
public DbSet<Cinema> Cinemas { get; set; }
public DbSet<ApplicationUser> applicationUsers { get; set; }

public DbSet<Actor> Actors { get; set; }
public DbSet<Producer> producers { get; set; }
public DbSet<Actors_Movies> Actors_Movies { get; set; }
public DbSet<Movie> Movies { get; set; }
    }
}
