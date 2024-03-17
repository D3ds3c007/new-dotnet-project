﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplication1.Data;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(apiContext))]
    partial class apiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApplication1.Models.Category", b =>
                {
                    b.Property<int>("idCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idCategory"));

                    b.Property<string>("categoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("idCategory");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("WebApplication1.Models.CategoryPicture", b =>
                {
                    b.Property<int>("idCategoryPicture")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idCategoryPicture"));

                    b.Property<int>("idCategory")
                        .HasColumnType("integer");

                    b.Property<int>("idPicture")
                        .HasColumnType("integer");

                    b.HasKey("idCategoryPicture");

                    b.HasIndex("idCategory");

                    b.HasIndex("idPicture");

                    b.ToTable("CategoryPicture");
                });

            modelBuilder.Entity("WebApplication1.Models.Comments", b =>
                {
                    b.Property<int>("idComment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idComment"));

                    b.Property<string>("comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("idPicture")
                        .HasColumnType("integer");

                    b.Property<int>("idUser")
                        .HasColumnType("integer");

                    b.HasKey("idComment");

                    b.HasIndex("idPicture");

                    b.HasIndex("idUser");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("WebApplication1.Models.Like", b =>
                {
                    b.Property<int>("idLike")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idLike"));

                    b.Property<int>("idPicture")
                        .HasColumnType("integer");

                    b.Property<int>("idUser")
                        .HasColumnType("integer");

                    b.HasKey("idLike");

                    b.HasIndex("idPicture");

                    b.HasIndex("idUser");

                    b.ToTable("Like");
                });

            modelBuilder.Entity("WebApplication1.Models.Picture", b =>
                {
                    b.Property<int>("idPicture")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idPicture"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("idUser")
                        .HasColumnType("integer");

                    b.Property<string>("picturePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("publishDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("views")
                        .HasColumnType("integer");

                    b.HasKey("idPicture");

                    b.HasIndex("idUser");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("WebApplication1.Models.User", b =>
                {
                    b.Property<int>("idUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idUser"));

                    b.Property<string>("bio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("pdpPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("pseudo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("pwd")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("idUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApplication1.Models.CategoryPicture", b =>
                {
                    b.HasOne("WebApplication1.Models.Category", "category")
                        .WithMany("categoryPictures")
                        .HasForeignKey("idCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Picture", "picture")
                        .WithMany("categoryPictures")
                        .HasForeignKey("idPicture")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");

                    b.Navigation("picture");
                });

            modelBuilder.Entity("WebApplication1.Models.Comments", b =>
                {
                    b.HasOne("WebApplication1.Models.Picture", "picture")
                        .WithMany("comments")
                        .HasForeignKey("idPicture")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.User", "User")
                        .WithMany("comments")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("picture");
                });

            modelBuilder.Entity("WebApplication1.Models.Like", b =>
                {
                    b.HasOne("WebApplication1.Models.Picture", "picture")
                        .WithMany("likes")
                        .HasForeignKey("idPicture")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.User", "user")
                        .WithMany("likes")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("picture");

                    b.Navigation("user");
                });

            modelBuilder.Entity("WebApplication1.Models.Picture", b =>
                {
                    b.HasOne("WebApplication1.Models.User", "user")
                        .WithMany("pictures")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("WebApplication1.Models.Category", b =>
                {
                    b.Navigation("categoryPictures");
                });

            modelBuilder.Entity("WebApplication1.Models.Picture", b =>
                {
                    b.Navigation("categoryPictures");

                    b.Navigation("comments");

                    b.Navigation("likes");
                });

            modelBuilder.Entity("WebApplication1.Models.User", b =>
                {
                    b.Navigation("comments");

                    b.Navigation("likes");

                    b.Navigation("pictures");
                });
#pragma warning restore 612, 618
        }
    }
}
