using EXSM3944_Demo.Migrations.PersonDatabase;
using EXSM3944_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace EXSM3944_Demo.Data
{
    public class PersonDatabaseContext : DbContext
    {
        public PersonDatabaseContext() { }

        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }

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

                entity
                    .HasIndex(job => job.JobID, $"FK_{nameof(Person)}_{nameof(Job)}");

            });
            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("jobs");
                entity.HasKey(model => model.ID);

                entity.Property(model => model.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(30)")
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(model => model.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(200)")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity
                    .HasMany(x => x.People)
                    .WithOne(y => y.Job)
                    .HasConstraintName($"FK_{nameof(Person)}_{nameof(Job)}")
                    .HasForeignKey(y => y.JobID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity
                    .HasIndex(job => job.IndustryID, $"FK_{nameof(Job)}_{nameof(Industry)}");
      

            });
            modelBuilder.Entity<Industry>(entity =>
            {
                entity.ToTable("industry");
                entity.HasKey(model => model.ID);

                entity.Property(model => model.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(30)")
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(model => model.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(200)")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity
                    .HasMany(x => x.Jobs)
                    .WithOne(y => y.Industry)
                    .HasConstraintName($"FK_{nameof(Job)}_{nameof(Industry)}")
                    .HasForeignKey(y => y.IndustryID)
                    .OnDelete(DeleteBehavior.Restrict);

            });
        }

        public DbSet<EXSM3944_Demo.Models.Industry>? Industry { get; set; }
    }
}
