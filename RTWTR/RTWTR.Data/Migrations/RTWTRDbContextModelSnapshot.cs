﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RTWTR.Data;
using System;

namespace RTWTR.Data.Migrations
{
    [DbContext(typeof(RTWTRDbContext))]
    partial class RTWTRDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RTWTR.Data.Models.Collection", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("RTWTR.Data.Models.CollectionTweet", b =>
                {
                    b.Property<string>("TweetId");

                    b.Property<string>("CollectionId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("TweetId", "CollectionId");

                    b.HasIndex("CollectionId");

                    b.ToTable("CollectionTweets");
                });

            modelBuilder.Entity("RTWTR.Data.Models.Tweet", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedAt")
                        .IsRequired();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("InReplyToScreenName");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("RetweetCount");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<string>("TweetId");

                    b.Property<string>("TwitterId")
                        .IsRequired();

                    b.Property<string>("TwitterUserId")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("TweetId");

                    b.HasIndex("TwitterUserId");

                    b.ToTable("Tweets");
                });

            modelBuilder.Entity("RTWTR.Data.Models.TwitterUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<string>("ProfileImageUrl");

                    b.Property<string>("ScreenName");

                    b.Property<string>("TwitterId");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("TwitterUsers");
                });

            modelBuilder.Entity("RTWTR.Data.Models.TwitterUserTweet", b =>
                {
                    b.Property<string>("TwitterUserId");

                    b.Property<string>("TweetId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<string>("UserId");

                    b.HasKey("TwitterUserId", "TweetId");

                    b.HasIndex("TweetId");

                    b.HasIndex("UserId");

                    b.ToTable("TwitterUserTweets");
                });

            modelBuilder.Entity("RTWTR.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("TwitterId");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("RTWTR.Data.Models.UserTweet", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("TweetId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("UserId", "TweetId");

                    b.HasIndex("TweetId");

                    b.ToTable("UserTweets");
                });

            modelBuilder.Entity("RTWTR.Data.Models.UserTwitterUser", b =>
                {
                    b.Property<string>("TwitterUserId");

                    b.Property<string>("UserId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("TwitterUserId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTwitterUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RTWTR.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RTWTR.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RTWTR.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RTWTR.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RTWTR.Data.Models.Collection", b =>
                {
                    b.HasOne("RTWTR.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RTWTR.Data.Models.CollectionTweet", b =>
                {
                    b.HasOne("RTWTR.Data.Models.Collection", "Collection")
                        .WithMany("CollectionTweets")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RTWTR.Data.Models.Tweet", "Tweet")
                        .WithMany("CollectionTweets")
                        .HasForeignKey("TweetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RTWTR.Data.Models.Tweet", b =>
                {
                    b.HasOne("RTWTR.Data.Models.Tweet")
                        .WithMany("Retweets")
                        .HasForeignKey("TweetId");

                    b.HasOne("RTWTR.Data.Models.TwitterUser", "TwitterUser")
                        .WithMany()
                        .HasForeignKey("TwitterUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RTWTR.Data.Models.TwitterUserTweet", b =>
                {
                    b.HasOne("RTWTR.Data.Models.Tweet", "Tweet")
                        .WithMany("TwitterUserTweets")
                        .HasForeignKey("TweetId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RTWTR.Data.Models.TwitterUser", "TwitterUser")
                        .WithMany("TwitterUserTweets")
                        .HasForeignKey("TwitterUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RTWTR.Data.Models.User")
                        .WithMany("TwitterUserTweets")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("RTWTR.Data.Models.UserTweet", b =>
                {
                    b.HasOne("RTWTR.Data.Models.Tweet", "Tweet")
                        .WithMany("UserTweets")
                        .HasForeignKey("TweetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RTWTR.Data.Models.User", "User")
                        .WithMany("UserTweets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RTWTR.Data.Models.UserTwitterUser", b =>
                {
                    b.HasOne("RTWTR.Data.Models.TwitterUser", "TwitterUser")
                        .WithMany("UserTwitterUsers")
                        .HasForeignKey("TwitterUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RTWTR.Data.Models.User", "User")
                        .WithMany("UserTwitterUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
