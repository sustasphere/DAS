﻿// <auto-generated />
using System;
using DAS.GoT.Behaviour.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAS.GoT.Behaviour.Migrations
{
    [DbContext(typeof(PersonContext))]
    [Migration("20241124092808_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("AllegiancePerson", b =>
                {
                    b.Property<int>("AllegiancesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AllegiancesId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("AllegiancePerson");
                });

            modelBuilder.Entity("BookPerson", b =>
                {
                    b.Property<int>("BooksId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BooksId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("BookPerson");
                });

            modelBuilder.Entity("DAS.GoT.Types.Entities.Alias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Aliases");
                });

            modelBuilder.Entity("DAS.GoT.Types.Entities.Allegiance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Allegiances");
                });

            modelBuilder.Entity("DAS.GoT.Types.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("DAS.GoT.Types.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Born")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Culture")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Died")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Father")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Mother")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Spouse")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("DAS.GoT.Types.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("DAS.GoT.Types.Entities.PovBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PovBooks");
                });

            modelBuilder.Entity("DAS.GoT.Types.Entities.Title", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Titles");
                });

            modelBuilder.Entity("DAS.GoT.Types.Entities.TvSerie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TvSeries");
                });

            modelBuilder.Entity("PersonPlayer", b =>
                {
                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayedById")
                        .HasColumnType("INTEGER");

                    b.HasKey("PersonId", "PlayedById");

                    b.HasIndex("PlayedById");

                    b.ToTable("PersonPlayer");
                });

            modelBuilder.Entity("PersonPovBook", b =>
                {
                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PovBooksId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PersonId", "PovBooksId");

                    b.HasIndex("PovBooksId");

                    b.ToTable("PersonPovBook");
                });

            modelBuilder.Entity("PersonTitle", b =>
                {
                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TitlesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PersonId", "TitlesId");

                    b.HasIndex("TitlesId");

                    b.ToTable("PersonTitle");
                });

            modelBuilder.Entity("PersonTvSerie", b =>
                {
                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TvSeriesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PersonId", "TvSeriesId");

                    b.HasIndex("TvSeriesId");

                    b.ToTable("PersonTvSerie");
                });

            modelBuilder.Entity("AllegiancePerson", b =>
                {
                    b.HasOne("DAS.GoT.Types.Entities.Allegiance", null)
                        .WithMany()
                        .HasForeignKey("AllegiancesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAS.GoT.Types.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookPerson", b =>
                {
                    b.HasOne("DAS.GoT.Types.Entities.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAS.GoT.Types.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAS.GoT.Types.Entities.Alias", b =>
                {
                    b.HasOne("DAS.GoT.Types.Entities.Person", null)
                        .WithMany("Aliases")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("PersonPlayer", b =>
                {
                    b.HasOne("DAS.GoT.Types.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAS.GoT.Types.Entities.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PersonPovBook", b =>
                {
                    b.HasOne("DAS.GoT.Types.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAS.GoT.Types.Entities.PovBook", null)
                        .WithMany()
                        .HasForeignKey("PovBooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PersonTitle", b =>
                {
                    b.HasOne("DAS.GoT.Types.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAS.GoT.Types.Entities.Title", null)
                        .WithMany()
                        .HasForeignKey("TitlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PersonTvSerie", b =>
                {
                    b.HasOne("DAS.GoT.Types.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAS.GoT.Types.Entities.TvSerie", null)
                        .WithMany()
                        .HasForeignKey("TvSeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAS.GoT.Types.Entities.Person", b =>
                {
                    b.Navigation("Aliases");
                });
#pragma warning restore 612, 618
        }
    }
}
