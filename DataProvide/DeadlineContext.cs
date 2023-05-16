﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DeadLine.DataProvide
{
    public partial class DeadlineContext : DbContext
    {
        public DeadlineContext()
        {
        }

        public DeadlineContext(DbContextOptions<DeadlineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseAnnouncement> CourseAnnouncements { get; set; }
        public virtual DbSet<CourseStatus> CourseStatuses { get; set; }
        public virtual DbSet<Deadline> Deadlines { get; set; }
        public virtual DbSet<DeadlineAnnouncement> DeadlineAnnouncements { get; set; }
        public virtual DbSet<DeadlinePenalty> DeadlinePenalties { get; set; }
        public virtual DbSet<Discussion> Discussions { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Professor> Professors { get; set; }
        public virtual DbSet<Reply> Replies { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.HasIndex(e => e.DiscussionId, "fk_Comment_Discussion1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.DiscussionId).HasColumnName("discussionId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(999)
                    .HasColumnName("value");

                entity.HasOne(d => d.Discussion)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.DiscussionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Comment_Discussion1");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.HasIndex(e => e.CourseStatusId, "fk_Course_CourseStatus1_idx");

                entity.HasIndex(e => e.ProfessorId, "fk_Course_Professor1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseStatusId).HasColumnName("courseStatusId");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.MaxSize).HasColumnName("maxSize");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("passwordHash");

                entity.Property(e => e.ProfessorId).HasColumnName("professorId");

                entity.Property(e => e.ShareId)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("shareId");

                entity.HasOne(d => d.CourseStatus)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CourseStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Course_CourseStatus1");

                entity.HasOne(d => d.Professor)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.ProfessorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Course_Professor1");
            });

            modelBuilder.Entity<CourseAnnouncement>(entity =>
            {
                entity.ToTable("CourseAnnouncement");

                entity.HasIndex(e => e.CourseId, "fk_CourseAnnouncement_Course1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnnounceDate).HasColumnName("announceDate");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasMaxLength(999)
                    .HasColumnName("data");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseAnnouncements)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CourseAnnouncement_Course1");
            });

            modelBuilder.Entity<CourseStatus>(entity =>
            {
                entity.ToTable("CourseStatus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Deadline>(entity =>
            {
                entity.ToTable("Deadline");

                entity.HasIndex(e => e.CourseId, "fk_Deadline_Course1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CloseDate).HasColumnName("closeDate");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.FileFormats)
                    .HasMaxLength(999)
                    .HasColumnName("fileFormats");

                entity.Property(e => e.OpenDate).HasColumnName("openDate");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Deadlines)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Deadline_Course1");
            });

            modelBuilder.Entity<DeadlineAnnouncement>(entity =>
            {
                entity.ToTable("DeadlineAnnouncement");

                entity.HasIndex(e => e.DeadlineId, "fk_DeadlineAnnouncement_Deadline1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnnounceDate).HasColumnName("announceDate");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.DeadlineId).HasColumnName("deadlineId");

                entity.HasOne(d => d.Deadline)
                    .WithMany(p => p.DeadlineAnnouncements)
                    .HasForeignKey(d => d.DeadlineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DeadlineAnnouncement_Deadline1");
            });

            modelBuilder.Entity<DeadlinePenalty>(entity =>
            {
                entity.ToTable("DeadlinePenalty");

                entity.HasIndex(e => e.DeadlineId, "fk_DeadlinePenalty_Deadline1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DeadlineId).HasColumnName("deadlineId");

                entity.Property(e => e.EndDate).HasColumnName("endDate");

                entity.Property(e => e.Wieght).HasColumnName("wieght");

                entity.HasOne(d => d.Deadline)
                    .WithMany(p => p.DeadlinePenalties)
                    .HasForeignKey(d => d.DeadlineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DeadlinePenalty_Deadline1");
            });

            modelBuilder.Entity<Discussion>(entity =>
            {
                entity.ToTable("Discussion");

                entity.HasIndex(e => e.CourseId, "fk_Discussion_Course1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.IsOpen)
                    .HasColumnType("tinyint")
                    .HasColumnName("isOpen");

                entity.Property(e => e.OpenDate).HasColumnName("openDate");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Discussions)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Discussion_Course1");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document");

                entity.HasIndex(e => e.DeadlineId, "fk_Document_Deadline1_idx");

                entity.HasIndex(e => e.StudentId, "fk_Document_Student1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.DeadlineId).HasColumnName("deadlineId");

                entity.Property(e => e.Format)
                    .HasMaxLength(45)
                    .HasColumnName("format");

                entity.Property(e => e.Grade).HasColumnName("grade");

                entity.Property(e => e.StudentId).HasColumnName("studentId");

                entity.Property(e => e.Url)
                    .HasMaxLength(999)
                    .HasColumnName("url");

                entity.HasOne(d => d.Deadline)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DeadlineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Document_Deadline1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Document_Student1");
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.ToTable("Professor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<Reply>(entity =>
            {
                entity.ToTable("Reply");

                entity.HasIndex(e => e.CommentId, "fk_Reply_Comment1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommentId).HasColumnName("commentId");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Reply_Comment1");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("StudentCourse");

                entity.HasIndex(e => e.CourseId, "fk_StudentCourse_Course1_idx");

                entity.HasIndex(e => e.StudentId, "fk_StudentCourse_Student1_idx");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.JoinDate).HasColumnName("joinDate");

                entity.Property(e => e.StudentId).HasColumnName("studentId");

                entity.HasOne(d => d.Course)
                    .WithMany()
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_StudentCourse_Course1");

                entity.HasOne(d => d.Student)
                    .WithMany()
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_StudentCourse_Student1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}