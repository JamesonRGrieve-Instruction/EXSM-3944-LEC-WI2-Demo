﻿using EXSM3944_Demo.Migrations.PersonDatabase;
using EXSM3944_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace EXSM3944_Demo.Data
{
    public class PersonDatabaseContext : DbContext
    {
        public PersonDatabaseContext() { }

        public virtual DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // If the context is not already configured:
            if (!optionsBuilder.IsConfigured)
            {
                // Specify the connection to the database.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=exsm3944_people", new MySqlServerVersion(new Version(10, 4, 24)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("people");
                entity.HasKey(model => model.ID);

                entity.Property(model => model.FirstName)
                    .HasColumnName("first_name")
                    .HasColumnType("varchar(30)")
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(model => model.LastName)
                    .HasColumnName("last_name")
                    .HasColumnType("varchar(30)")
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

            });
            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("jobs");
                entity.HasKey(model => model.ID);

                entity.Property(model => model.Name)
                    .HasColumnName("first_name")
                    .HasColumnType("varchar(30)")
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(model => model.Description)
                    .HasColumnName("last_name")
                    .HasColumnType("varchar(200)")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasMany(x => x.People).WithOne(y => y.Job).HasConstraintName("FK_Person_Job").HasForeignKey(y => y.JobID).OnDelete(DeleteBehavior.Restrict);

            });
        }

        public DbSet<EXSM3944_Demo.Models.Job>? Job { get; set; }
    }
}
