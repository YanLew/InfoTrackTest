﻿// <auto-generated />
using System;
using InfoTrackTest.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InfoTrackTest.Repositories.Migrations
{
    [DbContext(typeof(InfoTrackTestContext))]
    partial class InfoTrackTestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InfoTrackTest.Models.Entities.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Keyword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SearchEngineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SearchEngineId");

                    b.ToTable("History");
                });

            modelBuilder.Entity("InfoTrackTest.Models.Entities.SearchEngine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime");

                    b.Property<int?>("DefaultPageSize")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SearchEngine");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDateTime = new DateTime(2023, 3, 12, 12, 38, 1, 14, DateTimeKind.Utc).AddTicks(9992),
                            Name = "Google",
                            Url = "https://www.google.co.uk/search?num=##ResultNumber##&q=##SearchKeywords##",
                            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDateTime = new DateTime(2023, 3, 12, 12, 38, 1, 14, DateTimeKind.Utc).AddTicks(9997),
                            Name = "Bing",
                            Url = "https://www.bing.com/search?q=##SearchKeywords##&count=##ResultNumber##",
                            UserAgent = "Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko; compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm) Chrome/W.X.Y.Z Safari/537.36"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDateTime = new DateTime(2023, 3, 12, 12, 38, 1, 15, DateTimeKind.Utc).AddTicks(28),
                            DefaultPageSize = 7,
                            Name = "Yahoo",
                            Url = "https://uk.search.yahoo.com/search?p=##SearchKeywords##&b=##Offset##&pz=##DefaultOffsetSize##",
                            UserAgent = "Mozilla/5.0 (compatible; Yahoo! Slurp/3.0; http://help.yahoo.com/help/us/ysearch/slurp)"
                        });
                });

            modelBuilder.Entity("InfoTrackTest.Models.Entities.History", b =>
                {
                    b.HasOne("InfoTrackTest.Models.Entities.SearchEngine", "SearchEngine")
                        .WithMany("Histories")
                        .HasForeignKey("SearchEngineId")
                        .IsRequired();

                    b.Navigation("SearchEngine");
                });

            modelBuilder.Entity("InfoTrackTest.Models.Entities.SearchEngine", b =>
                {
                    b.Navigation("Histories");
                });
#pragma warning restore 612, 618
        }
    }
}
