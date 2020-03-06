using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DiabetesNotes.Models
{
    public class DataBaseContext : DbContext
    {

        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Measurement> Measurement { get; set;}
        public virtual DbSet<Day> Day { get; set; }
        public virtual DbSet<Month> Month { get; set; }
        public virtual DbSet<User> User { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(3)");
                entity.Property(e => e.Time).HasColumnType("datetime");
                entity.Property(e => e.Glucoze).HasColumnType("double(5, 2)");
                entity.HasOne(e => e._DayNum)
                .WithMany(d => d.Measurements)
                .HasForeignKey(e => e.DayNum)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Day_Id");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(3)");
                entity.Property(e => e.Name).HasColumnType("varchar(255)");
                entity.Property(e => e.Email).HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Day>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(3)");
                entity.Property(e => e.DayNum).HasColumnType("int(3)");
                entity.HasOne(e => e._MonthId)
                .WithMany(d => d.Days)
                .HasForeignKey(d => d.MonthId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Month_Id");
            });

            modelBuilder.Entity<Month>(entity =>
            {
                entity.Property(e => e.MonthNum).HasColumnType("int(3)");
            });


            



        }


    }
}
