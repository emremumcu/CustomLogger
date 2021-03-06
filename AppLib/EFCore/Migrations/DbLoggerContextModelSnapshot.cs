// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SGKWeb.Lib.EFCore;

namespace AppLib.EFCore.Migrations
{
    [DbContext(typeof(DbLoggerContext))]
    partial class DbLoggerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("AppLib.Entities.InternalLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EventId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExceptionMessage")
                        .HasColumnType("TEXT");

                    b.Property<string>("InnerExceptionMessage")
                        .HasColumnType("TEXT");

                    b.Property<string>("LogLevel")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RowTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("datetime()");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("InternalLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
