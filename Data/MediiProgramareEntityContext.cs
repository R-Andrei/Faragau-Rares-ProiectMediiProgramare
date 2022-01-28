#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediiProgramareEntity.Models;

namespace MediiProgramareEntity.Data
{
    public class MediiProgramareEntityContext : DbContext
    {
        public MediiProgramareEntityContext (DbContextOptions<MediiProgramareEntityContext> options)
            : base(options)
        {
        }

        public DbSet<MediiProgramareEntity.Models.GenreModel> GenreModel { get; set; }
        public DbSet<MediiProgramareEntity.Models.StudioModel> StudioModel { get; set; }
        public DbSet<MediiProgramareEntity.Models.MovieModel> MovieModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenreModel>()
                .Property(g => g.GenreId)
                .ValueGeneratedNever();
            modelBuilder.Entity<GenreModel>()
                .HasKey(g => g.GenreId);

            modelBuilder.Entity<StudioModel>()
                .Property(s => s.StudioId)
                .ValueGeneratedNever();
            modelBuilder.Entity<StudioModel>()
                .HasKey(s => s.StudioId);

            modelBuilder.Entity<MovieModel>()
                .Property(m => m.MovieId)
                .ValueGeneratedNever();
            modelBuilder.Entity<MovieModel>()
                .HasKey(m => m.MovieId);

            modelBuilder.Entity<MovieModel>()
                .HasOne(m => m.Genre)
                .WithMany(g => g.Movies)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MovieModel>()
                .HasOne(s => s.Studio)
                .WithMany(s => s.Movies)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<GenreModel>()
        //        .HasOne<MovieModel>(g => g.Movie)
        //        .WithOne(m => m.Genre)
        //        .HasForeignKey<MovieModel>(m => m.GenreId);

        //    modelBuilder.Entity<StudioModel>()
        //        .HasOne<MovieModel>(s => s.Movie)
        //        .WithOne(m => m.Studio)
        //        .HasForeignKey<MovieModel>(m => m.StudioId);


        //    modelBuilder.Entity<MovieModel>()
        //        .HasOne<GenreModel>(m => m.Genre)
        //        .WithOne(g => g.Movie)
        //        .HasForeignKey<MovieModel>(m => m.GenreId);

        //    modelBuilder.Entity<MovieModel>()
        //        .HasOne<StudioModel>(m => m.Studio)
        //        .WithOne(g => g.Movie)
        //        .HasForeignKey<MovieModel>(m => m.StudioId);


        //}
    }
}
