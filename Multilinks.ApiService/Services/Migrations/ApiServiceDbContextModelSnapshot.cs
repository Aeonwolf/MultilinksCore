﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Multilinks.ApiService.Services;

namespace Multilinks.ApiService.Services.Migrations
{
    [DbContext(typeof(ApiServiceDbContext))]
    partial class ApiServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<long>("HubConnectionId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<long>("OwnerEndpointOwnerId");

                    b.HasKey("EndpointId");

                    b.HasIndex("ClientEndpointClientId");

                    b.HasIndex("HubConnectionId");

                    b.HasIndex("OwnerEndpointOwnerId");

                    b.ToTable("Endpoints");
                });

            modelBuilder.Entity("Multilinks.ApiService.Entities.EndpointLinkEntity", b =>
                {
                    b.Property<Guid>("LinkId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AssociatedEndpointEndpointId");

                    b.Property<bool>("Confirmed");

                    b.Property<Guid?>("SourceEndpointEndpointId");

                    b.HasKey("LinkId");

                    b.HasIndex("AssociatedEndpointEndpointId");

                    b.HasIndex("SourceEndpointEndpointId");

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

                    b.Property<bool>("Connected");

                    b.Property<string>("ConnectionId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("HubConnections");
                });

            modelBuilder.Entity("Multilinks.ApiService.Entities.NotificationEntity", b =>
                {
                    b.Property<long>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Hidden");

                    b.Property<Guid>("Id");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("NotificationType");

                    b.Property<Guid>("RecipientEndpointEndpointId");

                    b.HasKey("NotificationId");

                    b.HasIndex("RecipientEndpointEndpointId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Multilinks.ApiService.Entities.EndpointEntity", b =>
                {
                    b.HasOne("Multilinks.ApiService.Entities.EndpointClientEntity", "Client")
                        .WithMany("EndpointEntities")
                        .HasForeignKey("ClientEndpointClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Multilinks.ApiService.Entities.HubConnectionEntity", "HubConnection")
                        .WithMany()
                        .HasForeignKey("HubConnectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Multilinks.ApiService.Entities.EndpointOwnerEntity", "Owner")
                        .WithMany("EndpointEntities")
                        .HasForeignKey("OwnerEndpointOwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Multilinks.ApiService.Entities.EndpointLinkEntity", b =>
                {
                    b.HasOne("Multilinks.ApiService.Entities.EndpointEntity", "AssociatedEndpoint")
                        .WithMany()
                        .HasForeignKey("AssociatedEndpointEndpointId");

                    b.HasOne("Multilinks.ApiService.Entities.EndpointEntity", "SourceEndpoint")
                        .WithMany()
                        .HasForeignKey("SourceEndpointEndpointId");
                });

            modelBuilder.Entity("Multilinks.ApiService.Entities.NotificationEntity", b =>
                {
                    b.HasOne("Multilinks.ApiService.Entities.EndpointEntity", "RecipientEndpoint")
                        .WithMany("NotificationEntities")
                        .HasForeignKey("RecipientEndpointEndpointId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
