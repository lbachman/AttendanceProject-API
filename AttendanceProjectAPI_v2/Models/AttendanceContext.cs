using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AttendanceProjectAPI_v2.Models;

public partial class AttendanceContext : DbContext
{
    public AttendanceContext()
    {
    }

    public AttendanceContext(DbContextOptions<AttendanceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attend> Attends { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Day> Days { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLevel> UserLevels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=127.0.0.1;Port=3306;User=root;Database=attendance;Pwd=Basscrunch@808;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attend>(entity =>
        {
            entity.HasKey(e => new { e.StudentUuid, e.ClassId, e.AttendanceDate }).HasName("PRIMARY");

            entity.ToTable("attends");

            entity.HasIndex(e => e.ClassId, "class_id");

            entity.Property(e => e.StudentUuid)
                .HasMaxLength(64)
                .HasColumnName("student_uuid");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.AttendanceDate)
                .HasColumnType("date")
                .HasColumnName("attendance_date");

            entity.HasOne(d => d.Class).WithMany(p => p.Attends)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("attends_ibfk_2");

            entity.HasOne(d => d.StudentUu).WithMany(p => p.Attends)
                .HasForeignKey(d => d.StudentUuid)
                .HasConstraintName("attends_ibfk_1");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PRIMARY");

            entity.ToTable("class");

            entity.HasIndex(e => e.InstructorId, "instructor_id");

            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.Days)
                .HasMaxLength(10)
                .HasColumnName("days");
            entity.Property(e => e.EndTime)
                .HasColumnType("time")
                .HasColumnName("end_time");
            entity.Property(e => e.InstructorId).HasColumnName("instructor_id");
            entity.Property(e => e.Room)
                .HasMaxLength(10)
                .HasColumnName("room");
            entity.Property(e => e.SemesterCode)
                .HasMaxLength(10)
                .HasColumnName("semester_code");
            entity.Property(e => e.StartTime)
                .HasColumnType("time")
                .HasColumnName("start_time");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Classes)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("class_ibfk_1");
        });

        modelBuilder.Entity<Day>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("days");

            entity.HasIndex(e => e.ClassId, "class_id");

            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.Day1)
                .HasMaxLength(10)
                .HasColumnName("day");

            entity.HasOne(d => d.Class).WithMany()
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("days_ibfk_1");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentUuid).HasName("PRIMARY");

            entity.ToTable("students");

            entity.Property(e => e.StudentUuid)
                .HasMaxLength(64)
                .HasColumnName("student_uuid");
            entity.Property(e => e.StudentUserName)
                .HasMaxLength(15)
                .HasColumnName("student_userName");

            entity.HasMany(d => d.Classes).WithMany(p => p.StudentUus)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentClass",
                    r => r.HasOne<Class>().WithMany()
                        .HasForeignKey("ClassId")
                        .HasConstraintName("student_class_ibfk_2"),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentUuid")
                        .HasConstraintName("student_class_ibfk_1"),
                    j =>
                    {
                        j.HasKey("StudentUuid", "ClassId").HasName("PRIMARY");
                        j.ToTable("student_class");
                        j.HasIndex(new[] { "ClassId" }, "class_id");
                        j.IndexerProperty<string>("StudentUuid")
                            .HasMaxLength(64)
                            .HasColumnName("student_uuid");
                        j.IndexerProperty<uint>("ClassId").HasColumnName("class_id");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(40)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<UserLevel>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user_level");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Level)
                .HasColumnType("enum('admin','instructor')")
                .HasColumnName("level");

            entity.HasOne(d => d.User).WithOne(p => p.UserLevel)
                .HasForeignKey<UserLevel>(d => d.UserId)
                .HasConstraintName("user_level_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
