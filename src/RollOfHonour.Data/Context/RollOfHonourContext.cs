using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RollOfHonour.Data.Models.DB;

namespace RollOfHonour.Data.Context;

public partial class RollOfHonourContext : DbContext
{
    public RollOfHonourContext()
    {
    }

    public RollOfHonourContext(DbContextOptions<RollOfHonourContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArmedService> ArmedServices { get; set; }

    public virtual DbSet<AuditItem> AuditItems { get; set; }

    public virtual DbSet<DataProblem> DataProblems { get; set; }

    public virtual DbSet<DataProblemAuditItem> DataProblemAuditItems { get; set; }

    public virtual DbSet<Decoration> Decorations { get; set; }

    public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonAuditItem> PersonAuditItems { get; set; }

    public virtual DbSet<PersonModeration> PersonModerations { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<PhotoAuditItem> PhotoAuditItems { get; set; }

    public virtual DbSet<PhotoModeration> PhotoModerations { get; set; }

    public virtual DbSet<RecordedName> RecordedNames { get; set; }

    public virtual DbSet<RecordedNameAuditItem> RecordedNameAuditItems { get; set; }

    public virtual DbSet<Regiment> Regiments { get; set; }

    public virtual DbSet<SubUnit> SubUnits { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<War> Wars { get; set; }

    public virtual DbSet<WarMemorial> WarMemorials { get; set; }

    public virtual DbSet<WarMemorialAuditItem> WarMemorialAuditItems { get; set; }

    public virtual DbSet<WebpagesMembership> WebpagesMemberships { get; set; }

    public virtual DbSet<WebpagesOauthMembership> WebpagesOauthMemberships { get; set; }

    // public virtual DbSet<WebpagesRole> WebpagesRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArmedService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ArmedServices");
        });

        modelBuilder.Entity<AuditItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AuditItems");

            entity.HasIndex(e => e.UserId, "IX_UserId");

            entity.Property(e => e.When).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.AuditItems)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_dbo.AuditItems_dbo.UserProfile_UserId");
        });

        modelBuilder.Entity<DataProblem>(entity =>
        {
            entity.HasKey(e => e.DataProblemId).HasName("PK_dbo.DataProblems");

            entity.Property(e => e.SubmittedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<DataProblemAuditItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.DataProblemAuditItems");

            entity.HasIndex(e => e.DataProblemId, "IX_DataProblemId");

            entity.HasIndex(e => e.Id, "IX_Id");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.DataProblem).WithMany(p => p.DataProblemAuditItems)
                .HasForeignKey(d => d.DataProblemId)
                .HasConstraintName("FK_dbo.DataProblemAuditItems_dbo.DataProblems_DataProblemId");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.DataProblemAuditItem)
                .HasForeignKey<DataProblemAuditItem>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.DataProblemAuditItems_dbo.AuditItems_Id");
        });

        modelBuilder.Entity<Decoration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Decorations");

            entity.HasMany(d => d.People).WithMany(p => p.Decorations)
                .UsingEntity<Dictionary<string, object>>(
                    "DecorationPersons",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("Person_Id")
                        .HasConstraintName("FK_dbo.DecorationPersons_dbo.People_Person_Id"),
                    l => l.HasOne<Decoration>().WithMany()
                        .HasForeignKey("Decoration_Id")
                        .HasConstraintName("FK_dbo.DecorationPersons_dbo.Decorations_Decoration_Id"),
                    j =>
                    {
                        j.HasKey("Decoration_Id", "Person_Id").HasName("PK_dbo.DecorationPersons");
                        j.HasIndex(new[] { "Decoration_Id" }, "IX_Decoration_Id");
                        j.HasIndex(new[] { "Person_Id" }, "IX_Person_Id");
                    });

            entity.HasMany(d => d.RecordedNames).WithMany(p => p.Decorations)
                .UsingEntity<Dictionary<string, object>>(
                    "DecorationRecordedNames",
                    r => r.HasOne<RecordedName>().WithMany()
                        .HasForeignKey("RecordedName_Id")
                        .HasConstraintName("FK_dbo.DecorationRecordedNames_dbo.RecordedNames_RecordedName_Id"),
                    l => l.HasOne<Decoration>().WithMany()
                        .HasForeignKey("Decoration_Id")
                        .HasConstraintName("FK_dbo.DecorationRecordedNames_dbo.Decorations_Decoration_Id"),
                    j =>
                    {
                        j.HasKey("Decoration_Id", "RecordedName_Id").HasName("PK_dbo.DecorationRecordedNames");
                        j.HasIndex(new[] { "Decoration_Id" }, "IX_Decoration_Id");
                        j.HasIndex(new[] { "RecordedName_Id" }, "IX_RecordedName_Id");
                    });
        });

        modelBuilder.Entity<MigrationHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("__MigrationHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(255);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.People");

            entity.HasIndex(e => e.ArmedServiceId, "IX_ArmedService_Id");

            entity.HasIndex(e => e.MainPhotoId, "IX_MainPhotoId");

            entity.HasIndex(e => e.SubUnitId, "IX_SubUnitId");

            entity.HasIndex(e => e.WarId, "IX_War_Id");

            entity.Property(e => e.ArmedServiceId).HasColumnName("ArmedService_Id");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.DateOfDeath).HasColumnType("datetime");
            entity.Property(e => e.WarId).HasColumnName("War_Id");

            entity.HasOne(d => d.ArmedService).WithMany(p => p.People)
                .HasForeignKey(d => d.ArmedServiceId)
                .HasConstraintName("FK_dbo.People_dbo.ArmedServices_ArmedService_Id");

            entity.HasOne(d => d.MainPhoto).WithMany(p => p.People)
                .HasForeignKey(d => d.MainPhotoId)
                .HasConstraintName("FK_dbo.People_dbo.Photos_MainPhotoId");

            entity.HasOne(d => d.SubUnit).WithMany(p => p.People)
                .HasForeignKey(d => d.SubUnitId)
                .HasConstraintName("FK_dbo.People_dbo.SubUnits_SubUnitId");

            entity.HasOne(d => d.War).WithMany(p => p.People)
                .HasForeignKey(d => d.WarId)
                .HasConstraintName("FK_dbo.People_dbo.Wars_War_Id");
        });

        modelBuilder.Entity<PersonAuditItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.PersonAuditItems");

            entity.HasIndex(e => e.Id, "IX_Id");

            entity.HasIndex(e => e.PersonId, "IX_PersonId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.PersonAuditItem)
                .HasForeignKey<PersonAuditItem>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.PersonAuditItems_dbo.AuditItems_Id");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonAuditItems)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_dbo.PersonAuditItems_dbo.People_PersonId");
        });

        modelBuilder.Entity<PersonModeration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.PersonModerations");

            entity.HasIndex(e => e.PersonId, "IX_PersonId");

            entity.HasIndex(e => e.SubUnitId, "IX_SubUnitId");

            entity.HasIndex(e => e.UserId, "IX_UserId");

            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.DateOfDeath).HasColumnType("datetime");
            entity.Property(e => e.ModerationComplete).HasColumnType("datetime");
            entity.Property(e => e.UploadDate).HasColumnType("datetime");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonModerations)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_dbo.PersonModerations_dbo.People_PersonId");

            entity.HasOne(d => d.SubUnit).WithMany(p => p.PersonModerations)
                .HasForeignKey(d => d.SubUnitId)
                .HasConstraintName("FK_dbo.PersonModerations_dbo.SubUnits_SubUnitId");

            entity.HasOne(d => d.User).WithMany(p => p.PersonModerations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_dbo.PersonModerations_dbo.UserProfile_UserId");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Photos");

            entity.HasIndex(e => e.PersonId, "IX_PersonId");

            entity.HasIndex(e => e.WarMemorialId, "IX_WarMemorialId");

            entity.HasOne(d => d.Person).WithMany(p => p.Photos)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_dbo.Photos_dbo.People_PersonId");

            entity.HasOne(d => d.WarMemorial).WithMany(p => p.Photos)
                .HasForeignKey(d => d.WarMemorialId)
                .HasConstraintName("FK_dbo.Photos_dbo.WarMemorials_WarMemorialId");
        });

        modelBuilder.Entity<PhotoAuditItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.PhotoAuditItems");

            entity.HasIndex(e => e.Id, "IX_Id");

            entity.HasIndex(e => e.PhotoId, "IX_PhotoId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.PhotoAuditItem)
                .HasForeignKey<PhotoAuditItem>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.PhotoAuditItems_dbo.AuditItems_Id");

            entity.HasOne(d => d.Photo).WithMany(p => p.PhotoAuditItems)
                .HasForeignKey(d => d.PhotoId)
                .HasConstraintName("FK_dbo.PhotoAuditItems_dbo.Photos_PhotoId");
        });

        modelBuilder.Entity<PhotoModeration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.PhotoModerations");

            entity.HasIndex(e => e.PersonId, "IX_PersonId");

            entity.HasIndex(e => e.UserId, "IX_UserId");

            entity.HasIndex(e => e.WarMemorialId, "IX_WarMemorialId");

            entity.Property(e => e.ModerationComplete).HasColumnType("datetime");
            entity.Property(e => e.UploadDate).HasColumnType("datetime");

            entity.HasOne(d => d.Person).WithMany(p => p.PhotoModerations)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_dbo.PhotoModerations_dbo.People_PersonId");

            entity.HasOne(d => d.User).WithMany(p => p.PhotoModerations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_dbo.PhotoModerations_dbo.UserProfile_UserId");

            entity.HasOne(d => d.WarMemorial).WithMany(p => p.PhotoModerations)
                .HasForeignKey(d => d.WarMemorialId)
                .HasConstraintName("FK_dbo.PhotoModerations_dbo.WarMemorials_WarMemorialId");
        });

        modelBuilder.Entity<RecordedName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.RecordedNames");

            entity.HasIndex(e => e.ArmedServiceId, "IX_ArmedServiceId");

            entity.HasIndex(e => e.PersonId, "IX_PersonId");

            entity.HasIndex(e => e.SubUnitId, "IX_SubUnitId");

            entity.HasIndex(e => e.WarId, "IX_WarId");

            entity.HasIndex(e => e.WarMemorialId, "IX_WarMemorialId");

            entity.Property(e => e.IwmnameRefNo).HasColumnName("IWMNameRefNo");

            entity.HasOne(d => d.ArmedService).WithMany(p => p.RecordedNames)
                .HasForeignKey(d => d.ArmedServiceId)
                .HasConstraintName("FK_dbo.RecordedNames_dbo.ArmedServices_ArmedServiceId");

            entity.HasOne(d => d.Person).WithMany(p => p.RecordedNames)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_dbo.RecordedNames_dbo.People_PersonId");

            entity.HasOne(d => d.SubUnit).WithMany(p => p.RecordedNames)
                .HasForeignKey(d => d.SubUnitId)
                .HasConstraintName("FK_dbo.RecordedNames_dbo.SubUnits_SubUnitId");

            entity.HasOne(d => d.War).WithMany(p => p.RecordedNames)
                .HasForeignKey(d => d.WarId)
                .HasConstraintName("FK_dbo.RecordedNames_dbo.Wars_WarId");

            entity.HasOne(d => d.WarMemorial).WithMany(p => p.RecordedNames)
                .HasForeignKey(d => d.WarMemorialId)
                .HasConstraintName("FK_dbo.RecordedNames_dbo.WarMemorials_WarMemorialId");
        });

        modelBuilder.Entity<RecordedNameAuditItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.RecordedNameAuditItems");

            entity.HasIndex(e => e.Id, "IX_Id");

            entity.HasIndex(e => e.RecordedNameId, "IX_RecordedNameId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.RecordedNameAuditItem)
                .HasForeignKey<RecordedNameAuditItem>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.RecordedNameAuditItems_dbo.AuditItems_Id");

            entity.HasOne(d => d.RecordedName).WithMany(p => p.RecordedNameAuditItems)
                .HasForeignKey(d => d.RecordedNameId)
                .HasConstraintName("FK_dbo.RecordedNameAuditItems_dbo.RecordedNames_RecordedNameId");
        });

        modelBuilder.Entity<Regiment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Regiments");
        });

        modelBuilder.Entity<SubUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.SubUnits");

            entity.HasIndex(e => e.RegimentId, "IX_RegimentId");

            entity.HasOne(d => d.Regiment).WithMany(p => p.SubUnits)
                .HasForeignKey(d => d.RegimentId)
                .HasConstraintName("FK_dbo.SubUnits_dbo.Regiments_RegimentId");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_dbo.UserProfile");

            entity.ToTable("UserProfile");

            // entity.HasMany(d => d.Roles).WithMany(p => p.Users)
            //     .UsingEntity<Dictionary<string, object>>(
            //         "WebpagesUsersInRole",
            //         r => r.HasOne<WebpagesRole>().WithMany()
            //             .HasForeignKey("RoleId")
            //             .OnDelete(DeleteBehavior.ClientSetNull)
            //             .HasConstraintName("fk_RoleId"),
            //         l => l.HasOne<UserProfile>().WithMany()
            //             .HasForeignKey("UserId")
            //             .OnDelete(DeleteBehavior.ClientSetNull)
            //             .HasConstraintName("fk_UserId"),
            //         j =>
            //         {
            //             j.HasKey("UserId", "RoleId").HasName("PK__webpages__AF2760AD571DF1D5");
            //             j.ToTable("webpages_UsersInRoles");
            //         });

            entity.HasMany(d => d.WarMemorials).WithMany(p => p.UserProfileUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserProfileWarMemorial",
                    r => r.HasOne<WarMemorial>().WithMany()
                        .HasForeignKey("WarMemorialId")
                        .HasConstraintName("FK_dbo.UserProfileWarMemorials_dbo.WarMemorials_WarMemorial_Id"),
                    l => l.HasOne<UserProfile>().WithMany()
                        .HasForeignKey("UserProfileUserId")
                        .HasConstraintName("FK_dbo.UserProfileWarMemorials_dbo.UserProfile_UserProfile_UserId"),
                    j =>
                    {
                        j.HasKey("UserProfileUserId", "WarMemorialId").HasName("PK_dbo.UserProfileWarMemorials");
                        j.HasIndex(new[] { "UserProfileUserId" }, "IX_UserProfile_UserId");
                        j.HasIndex(new[] { "WarMemorialId" }, "IX_WarMemorial_Id");
                    });
        });

        modelBuilder.Entity<War>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Wars");
        });

        modelBuilder.Entity<WarMemorial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.WarMemorials");

            entity.HasIndex(e => e.MainPhotoId, "IX_MainPhotoId");

            entity.Property(e => e.NamesCount).HasDefaultValueSql("((1))");
            entity.Property(e => e.Ukniwmref).HasColumnName("UKNIWMRef");
        });

        modelBuilder.Entity<WarMemorialAuditItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.WarMemorialAuditItems");

            entity.HasIndex(e => e.Id, "IX_Id");

            entity.HasIndex(e => e.WarMemorialId, "IX_WarMemorialId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.WarMemorialAuditItem)
                .HasForeignKey<WarMemorialAuditItem>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.WarMemorialAuditItems_dbo.AuditItems_Id");

            entity.HasOne(d => d.WarMemorial).WithMany(p => p.WarMemorialAuditItems)
                .HasForeignKey(d => d.WarMemorialId)
                .HasConstraintName("FK_dbo.WarMemorialAuditItems_dbo.WarMemorials_WarMemorialId");
        });

        modelBuilder.Entity<WebpagesMembership>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__webpages__1788CC4C4AB81AF0");

            entity.ToTable("webpages_Membership");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.ConfirmationToken).HasMaxLength(128);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.IsConfirmed).HasDefaultValueSql("((0))");
            entity.Property(e => e.LastPasswordFailureDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(128);
            entity.Property(e => e.PasswordChangedDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordSalt).HasMaxLength(128);
            entity.Property(e => e.PasswordVerificationToken).HasMaxLength(128);
            entity.Property(e => e.PasswordVerificationTokenExpirationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<WebpagesOauthMembership>(entity =>
        {
            entity.HasKey(e => new { e.Provider, e.ProviderUserId }).HasName("PK__webpages__F53FC0ED46E78A0C");

            entity.ToTable("webpages_OAuthMembership");

            entity.Property(e => e.Provider).HasMaxLength(30);
            entity.Property(e => e.ProviderUserId).HasMaxLength(100);
        });

        // modelBuilder.Entity<WebpagesRole>(entity =>
        // {
        //     entity.HasKey(e => e.RoleId).HasName("PK__webpages__8AFACE1A5070F446");
        //
        //     entity.ToTable("webpages_Roles");
        //
        //     entity.HasIndex(e => e.RoleName, "UQ__webpages__8A2B6160534D60F1").IsUnique();
        //
        //     entity.Property(e => e.RoleName).HasMaxLength(256);
        // });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
