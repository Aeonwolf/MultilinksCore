﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Multilinks.ApiService.Services;

namespace Multilinks.ApiService.Services.Migrations
{
    [DbContext(typeof(ApiServiceDbContext))]
    [Migration("20190303083115_UpdateEndpointEntityWithNavigationProperties")]
    partial class UpdateEndpointEntityWithNavigationProperties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Multilinks.ApiService.Entities.EndpointClientEntity", b =>
                {
                    b.Property<long>("EndpointClientId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("ClientType")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("EndpointClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Multilinks.ApiService.Entities.EndpointEntity", b =>
                {
                    b.Property<Guid>("EndpointId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ClientEndpointClientId");

                    b.Property<string>("Description")
                        .HasMaxLength(512);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<long>("OwnerEndpointOwnerId");

                    b.HasKey("EndpointId");

                    b.HasIndex("ClientEndpointClientId");

                    b.HasIndex("OwnerEndpointOwnerId");

                    b.ToTable("Endpoints");
                });

            modelBuilder.Entity("Multilinks.ApiService.Entities.EndpointLinkEntity", b =>
                {
                    b.Property<Guid>("LinkId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AssociatedEndpointId");

                    b.Property<Guid>("SourceEndpointId");

                    b.Property<string>("Status");

                    b.HasKey("LinkId");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("Multilinks.ApiService.Entities.EndpointOwnerEntity", b =>
                {
                    b.Property<long>("EndpointOwnerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("IdentityId");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("EndpointOwnerId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("Multilinks.ApiService.Entities.HubConnectionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConnectionId");

                    b.Property<Guid>("EndpointId");

                    b.HasKey("Id");

                    b.ToTable("HubConnections");
                });

            modelBuilder.Entity("Multilinks.ApiService.Entities.EndpointEntity", b =>
                {
                    b.HasOne("Multilinks.ApiService.Entities.EndpointClientEntity", "Client")
                        .WithMany("EndpointEntities")
                        .HasForeignKey("ClientEndpointClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Multilinks.ApiService.Entities.EndpointOwnerEntity", "Owner")
                        .WithMany("EndpointEntities")
                        .HasForeignKey("OwnerEndpointOwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
