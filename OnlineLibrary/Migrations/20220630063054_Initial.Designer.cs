// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineLibrary.Data;

#nullable disable

namespace OnlineLibrary.Migrations
{
    [DbContext(typeof(OnlineLibraryDbContext))]
    [Migration("20220630063054_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OnlineLibrary.Models.Book", b =>
                {
                    b.Property<string>("BookID")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("BookDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("BookFaculty")
                        .HasColumnType("int");

                    b.Property<int>("BookFileType")
                        .HasColumnType("int");

                    b.Property<string>("BookTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PathToBookFile")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TeacherID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("BookID");

                    b.HasIndex("TeacherID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("OnlineLibrary.Models.Student", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("StudentFatherName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("StudentMotherName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TeacherID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserID");

                    b.HasIndex("TeacherID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("OnlineLibrary.Models.Teacher", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TeacherDegree")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TeacherWorkExperience")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserID");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("OnlineLibrary.Models.User", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PathToUserPhoto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserAddress")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UserDateofBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserEmail")
                        .HasColumnType("longtext");

                    b.Property<int>("UserFaculty")
                        .HasColumnType("int");

                    b.Property<int>("UserGender")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserPasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.Property<string>("UserTel")
                        .HasColumnType("longtext");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OnlineLibrary.Models.Video", b =>
                {
                    b.Property<string>("VideoID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PathToVideoFile")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PathToVideoThumbnail")
                        .HasColumnType("longtext");

                    b.Property<string>("TeacherID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("VideoDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("VideoFaculty")
                        .HasColumnType("int");

                    b.Property<string>("VideoTitle")
                        .HasColumnType("longtext");

                    b.HasKey("VideoID");

                    b.HasIndex("TeacherID");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("OnlineLibrary.Models.Book", b =>
                {
                    b.HasOne("OnlineLibrary.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("OnlineLibrary.Models.Student", b =>
                {
                    b.HasOne("OnlineLibrary.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineLibrary.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineLibrary.Models.Teacher", b =>
                {
                    b.HasOne("OnlineLibrary.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineLibrary.Models.Video", b =>
                {
                    b.HasOne("OnlineLibrary.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });
#pragma warning restore 612, 618
        }
    }
}
