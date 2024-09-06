﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetFamily.Infrastucture;

#nullable disable

namespace PetFamily.Infrastucture.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240905225247_deleteNumberPets")]
    partial class deleteNumberPets
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetFamily.Domain.PetMenegment.Entity.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_of_creation");

                    b.Property<bool>("IsCastrated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_castrated");

                    b.Property<bool>("IsVaccinated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccinated");

                    b.Property<Guid?>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "PetFamily.Domain.PetMenegment.Entity.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("ApartmentNumber")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("apartment_number");

                            b1.Property<string>("Flat")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("flat");

                            b1.Property<string>("House")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("house");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("street");

                            b1.Property<string>("Сity")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("city");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("AssistanceStatus", "PetFamily.Domain.PetMenegment.Entity.Pet.AssistanceStatus#AssistanceStatus", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Status")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("status");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Color", "PetFamily.Domain.PetMenegment.Entity.Pet.Color#Color", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("color");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetFamily.Domain.PetMenegment.Entity.Pet.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .HasMaxLength(6000)
                                .HasColumnType("character varying(6000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("HealthInformation", "PetFamily.Domain.PetMenegment.Entity.Pet.HealthInformation#HealthInformation", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .HasMaxLength(3000)
                                .HasColumnType("character varying(3000)")
                                .HasColumnName("health_information");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Nickname", "PetFamily.Domain.PetMenegment.Entity.Pet.Nickname#Nickname", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("nickname");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PetFamily.Domain.PetMenegment.Entity.Pet.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("phone_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Size", "PetFamily.Domain.PetMenegment.Entity.Pet.Size#Size", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<double>("Height")
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("double precision")
                                .HasColumnName("height");

                            b1.Property<double>("Weight")
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("double precision")
                                .HasColumnName("height");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("SpeciesAndBreed", "PetFamily.Domain.PetMenegment.Entity.Pet.SpeciesAndBreed#SpeciesAndBreed", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<Guid>("BreedId")
                                .HasColumnType("uuid")
                                .HasColumnName("breed_id");

                            b1.Property<Guid>("SpeciesId")
                                .HasColumnType("uuid")
                                .HasColumnName("SpeciesId");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pet");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pet_volunteer_id");

                    b.ToTable("pet", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.PetMenegment.Entity.PetPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsMain")
                        .HasMaxLength(100)
                        .HasColumnType("boolean")
                        .HasColumnName("is_main");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("path");

                    b.Property<Guid?>("pet_id")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.HasKey("Id")
                        .HasName("pk_pet_photo");

                    b.HasIndex("pet_id")
                        .HasDatabaseName("ix_pet_photo_pet_id");

                    b.ToTable("pet_photo", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.PetMenegment.Entity.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetFamily.Domain.PetMenegment.Entity.Volunteer.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .HasMaxLength(6000)
                                .HasColumnType("character varying(6000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "PetFamily.Domain.PetMenegment.Entity.Volunteer.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");

                            b1.Property<string>("Patronymic")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("patronymic");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("surname");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PetFamily.Domain.PetMenegment.Entity.Volunteer.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("phone_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("YearsExperience", "PetFamily.Domain.PetMenegment.Entity.Volunteer.YearsExperience#YearsExperience", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("years_experience");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteer");

                    b.ToTable("volunteer", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.SpeciesMenegment.Entity.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("breed_id")
                        .HasColumnType("uuid")
                        .HasColumnName("breed_id");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetFamily.Domain.SpeciesMenegment.Entity.Breed.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_breed");

                    b.HasIndex("breed_id")
                        .HasDatabaseName("ix_breed_breed_id");

                    b.ToTable("breed", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.SpeciesMenegment.Entity.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetFamily.Domain.SpeciesMenegment.Entity.Species.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("species", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.PetMenegment.Entity.Pet", b =>
                {
                    b.HasOne("PetFamily.Domain.PetMenegment.Entity.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .HasConstraintName("fk_pet_volunteer_volunteer_id");

                    b.OwnsOne("PetFamily.Domain.PetMenegment.ValueObjects.PetDetailsForAssistance", "DetailsForAssistance", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("PetId");

                            b1.ToTable("pet");

                            b1.ToJson("DetailsForAssistance");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pet_pet_id");

                            b1.OwnsMany("PetFamily.Domain.PetMenegment.ValueObjects.DetailsForAssistance", "DetailsForAssistance", b2 =>
                                {
                                    b2.Property<Guid>("PetDetailsForAssistancePetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(6000)
                                        .HasColumnType("character varying(6000)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(6000)
                                        .HasColumnType("character varying(6000)");

                                    b2.HasKey("PetDetailsForAssistancePetId", "Id")
                                        .HasName("pk_pet");

                                    b2.ToTable("pet");

                                    b2.WithOwner()
                                        .HasForeignKey("PetDetailsForAssistancePetId")
                                        .HasConstraintName("fk_pet_pet_pet_details_for_assistance_pet_id");
                                });

                            b1.Navigation("DetailsForAssistance");
                        });

                    b.Navigation("DetailsForAssistance")
                        .IsRequired();
                });

            modelBuilder.Entity("PetFamily.Domain.PetMenegment.Entity.PetPhoto", b =>
                {
                    b.HasOne("PetFamily.Domain.PetMenegment.Entity.Pet", null)
                        .WithMany("PetPhotos")
                        .HasForeignKey("pet_id")
                        .HasConstraintName("fk_pet_photo_pet_pet_id");
                });

            modelBuilder.Entity("PetFamily.Domain.PetMenegment.Entity.Volunteer", b =>
                {
                    b.OwnsOne("PetFamily.Domain.PetMenegment.ValueObjects.VolunteerDetailsForAssistance", "DetailsForAssistance", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteer");

                            b1.ToJson("DetailsForAssistance");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteer_volunteer_id");

                            b1.OwnsMany("PetFamily.Domain.PetMenegment.ValueObjects.DetailsForAssistance", "DetailsForAssistance", b2 =>
                                {
                                    b2.Property<Guid>("VolunteerDetailsForAssistanceVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .HasMaxLength(6000)
                                        .HasColumnType("character varying(6000)");

                                    b2.Property<string>("Name")
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.HasKey("VolunteerDetailsForAssistanceVolunteerId", "Id")
                                        .HasName("pk_volunteer");

                                    b2.ToTable("volunteer");

                                    b2.WithOwner()
                                        .HasForeignKey("VolunteerDetailsForAssistanceVolunteerId")
                                        .HasConstraintName("fk_volunteer_volunteer_volunteer_details_for_assistance_volunteer_id");
                                });

                            b1.Navigation("DetailsForAssistance");
                        });

                    b.OwnsOne("PetFamily.Domain.PetMenegment.ValueObjects.VolunteerSocialNetwork", "SocialNetwork", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteer");

                            b1.ToJson("SocialNetwork");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteer_volunteer_id");

                            b1.OwnsMany("PetFamily.Domain.PetMenegment.ValueObjects.SocialNetwork", "SocialNetwork", b2 =>
                                {
                                    b2.Property<Guid>("VolunteerSocialNetworkVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Link")
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("Name")
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)");

                                    b2.HasKey("VolunteerSocialNetworkVolunteerId", "Id")
                                        .HasName("pk_volunteer");

                                    b2.ToTable("volunteer");

                                    b2.WithOwner()
                                        .HasForeignKey("VolunteerSocialNetworkVolunteerId")
                                        .HasConstraintName("fk_volunteer_volunteer_volunteer_social_network_volunteer_id");
                                });

                            b1.Navigation("SocialNetwork");
                        });

                    b.Navigation("DetailsForAssistance");

                    b.Navigation("SocialNetwork");
                });

            modelBuilder.Entity("PetFamily.Domain.SpeciesMenegment.Entity.Breed", b =>
                {
                    b.HasOne("PetFamily.Domain.SpeciesMenegment.Entity.Species", null)
                        .WithMany("breeds")
                        .HasForeignKey("breed_id")
                        .HasConstraintName("fk_breed_species_breed_id");
                });

            modelBuilder.Entity("PetFamily.Domain.PetMenegment.Entity.Pet", b =>
                {
                    b.Navigation("PetPhotos");
                });

            modelBuilder.Entity("PetFamily.Domain.PetMenegment.Entity.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("PetFamily.Domain.SpeciesMenegment.Entity.Species", b =>
                {
                    b.Navigation("breeds");
                });
#pragma warning restore 612, 618
        }
    }
}
