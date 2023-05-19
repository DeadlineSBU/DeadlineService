using System;
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
        public virtual DbSet<Deadline> Deadlines { get; set; }
        public virtual DbSet<DeadlineAnnouncement> DeadlineAnnouncements { get; set; }
        public virtual DbSet<DeadlinePenalty> DeadlinePenalties { get; set; }
        public virtual DbSet<Discussion> Discussions { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Reply> Replies { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server=10.51.10.137;Port=3320;Database=Deadline;Uid=user;Pwd=deadline");
            }
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

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("userId");

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

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseStatus).HasColumnName("courseStatus");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.Description)
                    .HasMaxLength(999)
                    .HasColumnName("description");

                entity.Property(e => e.MaxSize).HasColumnName("maxSize");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("password");

                entity.Property(e => e.ProfessorId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("professorId");

                entity.Property(e => e.ShareId)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("shareId");
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

            modelBuilder.Entity<Deadline>(entity =>
            {
                entity.ToTable("Deadline");

                entity.HasIndex(e => e.CourseId, "fk_Deadline_Course1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CloseDate).HasColumnName("closeDate");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.Description)
                    .HasMaxLength(999)
                    .HasColumnName("description");

                entity.Property(e => e.FileFormats)
                    .HasMaxLength(999)
                    .HasColumnName("fileFormats");

                entity.Property(e => e.OpenDate).HasColumnName("openDate");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("title");

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

                entity.Property(e => e.Title)
                    .HasMaxLength(99)
                    .HasColumnName("title");

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

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.DeadlineId).HasColumnName("deadlineId");

                entity.Property(e => e.Format)
                    .HasMaxLength(45)
                    .HasColumnName("format");

                entity.Property(e => e.Grade).HasColumnName("grade");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("studentId");

                entity.Property(e => e.Url)
                    .HasMaxLength(999)
                    .HasColumnName("url");

                entity.HasOne(d => d.Deadline)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DeadlineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Document_Deadline1");
            });

            modelBuilder.Entity<Reply>(entity =>
            {
                entity.ToTable("Reply");

                entity.HasIndex(e => e.CommentId, "fk_Reply_Comment1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommentId).HasColumnName("commentId");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("userId");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(999)
                    .HasColumnName("value");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Reply_Comment1");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.ToTable("StudentCourse");

                entity.HasIndex(e => e.CourseId, "fk_StudentCourse_Course1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.JoinDate).HasColumnName("joinDate");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("studentId");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_StudentCourse_Course1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
