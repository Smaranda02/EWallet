using System;
using System.Collections.Generic;
using EWallet.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DataAccess.EntityFramework;

public partial class EWalletContext : DbContext
{
    public EWalletContext()
    {
    }

    public EWalletContext(DbContextOptions<EWalletContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Friendship> Friendships { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<ImmediateTransaction> ImmediateTransactions { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<PiggyBank> PiggyBanks { get; set; }

    public virtual DbSet<PiggyBankStatusType> PiggyBankStatusTypes { get; set; }

    public virtual DbSet<PiggyBanksFriend> PiggyBanksFriends { get; set; }

    public virtual DbSet<PiggyBanksIncome> PiggyBanksIncomes { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<RecurrenceType> RecurrenceTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Spending> Spendings { get; set; }

    public virtual DbSet<SpendingCategory> SpendingCategories { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VwSpendingCategoriesCount> VwSpendingCategoriesCounts { get; set; }

    public virtual DbSet<VwUpcomingBirthday> VwUpcomingBirthdays { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-9E5HMJQ\\SQL_SERVER;Initial Catalog=EWallet;Integrated Security=true;TrustServerCertificate= true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Comments_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CommentDateTime).HasColumnType("datetime");
            entity.Property(e => e.CommentMessage).HasMaxLength(500);
            entity.Property(e => e.ParentCommentId).HasColumnName("ParentCommentID");
            entity.Property(e => e.PostId).HasColumnName("PostID");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Comments_ParentCommentID_FK");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("Comments_PostID_FK");
        });

        modelBuilder.Entity<Friendship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Friendship_PK");

            entity.ToTable("Friendship");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.User1Id).HasColumnName("User1ID");
            entity.Property(e => e.User2Id).HasColumnName("User2ID");

            entity.HasOne(d => d.User1).WithMany(p => p.FriendshipUser1s)
                .HasForeignKey(d => d.User1Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Friendship_User1ID_FK");

            entity.HasOne(d => d.User2).WithMany(p => p.FriendshipUser2s)
                .HasForeignKey(d => d.User2Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Friendship_User2ID_FK");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Images_PK");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ImageName).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Image)
                .HasForeignKey<Image>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Images_ID_FK");

            entity.HasOne(d => d.User).WithMany(p => p.Images)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Images_UserID_FK");
        });

        modelBuilder.Entity<ImmediateTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ImmediateTransactions_PK");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ImmediateTrasactionDateTime).HasColumnType("datetime");
            entity.Property(e => e.ImmediateTrasactionSum).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IncomeId).HasColumnName("IncomeID");
            entity.Property(e => e.SpendingId).HasColumnName("SpendingID");
            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Income).WithMany(p => p.ImmediateTransactions)
                .HasForeignKey(d => d.IncomeId)
                .HasConstraintName("ImmediateTransactions_IncomeID_FK");

            entity.HasOne(d => d.Spending).WithMany(p => p.ImmediateTransactions)
                .HasForeignKey(d => d.SpendingId)
                .HasConstraintName("ImmediateTransactions_SpendingID_FK");

            entity.HasOne(d => d.Transaction).WithMany(p => p.ImmediateTransactions)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("ImmediateTransactions_TransactionID_FK");

            entity.HasOne(d => d.User).WithMany(p => p.ImmediateTransactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("ImmediateTransactions_UserID_FK");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Incomes_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IncomeSum).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            entity.Property(e => e.RecurrenceTypeId).HasColumnName("RecurrenceTypeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.RecurrenceType).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.RecurrenceTypeId)
                .HasConstraintName("Incomes_RecurrenceTypeID_FK");

            entity.HasOne(d => d.User).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Incomes_UserID_FK");
        });

        modelBuilder.Entity<PiggyBank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PiggyBanks_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.CurrentBalance)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.PiggyBankStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.TargetSum).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Creator).WithMany(p => p.PiggyBanks)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PiggyBanks_CreatorID_FK");

            entity.HasOne(d => d.PiggyBankStatusNavigation).WithMany(p => p.PiggyBanks)
                .HasForeignKey(d => d.PiggyBankStatus)
                .HasConstraintName("PiggyBanks_PiggyBankStatus_FK");
        });

        modelBuilder.Entity<PiggyBankStatusType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PiggyBankStatusTypes_PK");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.StatusName).HasMaxLength(100);
        });

        modelBuilder.Entity<PiggyBanksFriend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PiggyBanksFriends_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PiggyBankId).HasColumnName("PiggyBankID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.PiggyBank).WithMany(p => p.PiggyBanksFriends)
                .HasForeignKey(d => d.PiggyBankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PiggyBanksFriends_PiggyBankID_FK");

            entity.HasOne(d => d.User).WithMany(p => p.PiggyBanksFriends)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PiggyBanksFriends_CreatorID_FK");
        });

        modelBuilder.Entity<PiggyBanksIncome>(entity =>
        {
            entity.HasKey(e => new { e.IncomeId, e.PiggyBankId }).HasName("PiggyBanksIncome_PK");

            entity.ToTable("PiggyBanksIncome");

            entity.Property(e => e.IncomeId).HasColumnName("IncomeID");
            entity.Property(e => e.PiggyBankId).HasColumnName("PiggyBankID");
            entity.Property(e => e.AllocatedIncomeAmount)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Income).WithMany(p => p.PiggyBanksIncomes)
                .HasForeignKey(d => d.IncomeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PiggyBanksIncome_IncomeID_FK");

            entity.HasOne(d => d.PiggyBank).WithMany(p => p.PiggyBanksIncomes)
                .HasForeignKey(d => d.PiggyBankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PiggyBanksIncome_PiggyBankID_FK");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Posts_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PostDateTime).HasColumnType("datetime");
            entity.Property(e => e.PostMessage).HasMaxLength(500);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Posts_UserID_FK");
        });

        modelBuilder.Entity<RecurrenceType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RecurrenceTypes_PK");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.RecurrenceTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Roles_PK");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Spending>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Spendings_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RecurrenceTypeId).HasColumnName("RecurrenceTypeID");
            entity.Property(e => e.SpendingCategoryId).HasColumnName("SpendingCategoryID");
            entity.Property(e => e.SpendingDescription).HasMaxLength(500);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.RecurrenceType).WithMany(p => p.Spendings)
                .HasForeignKey(d => d.RecurrenceTypeId)
                .HasConstraintName("Spendings_RecurrenceTypeID_FK");

            entity.HasOne(d => d.SpendingCategory).WithMany(p => p.Spendings)
                .HasForeignKey(d => d.SpendingCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Spendings_SpendingCategoryID_FK");

            entity.HasOne(d => d.User).WithMany(p => p.Spendings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Spendings_UserID_FK");
        });

        modelBuilder.Entity<SpendingCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SpendingCategories_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TRansaction_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IncomeId).HasColumnName("IncomeID");
            entity.Property(e => e.PiggyBankId).HasColumnName("PiggyBankID");
            entity.Property(e => e.SpendingId).HasColumnName("SpendingID");
            entity.Property(e => e.TrasactionDateTime).HasColumnType("datetime");
            entity.Property(e => e.TrasactionSum).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Income).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.IncomeId)
                .HasConstraintName("Transactions_IncomeID_FK");

            entity.HasOne(d => d.PiggyBank).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PiggyBankId)
                .HasConstraintName("Transactions_PiggyBankID_FK");

            entity.HasOne(d => d.Spending).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.SpendingId)
                .HasConstraintName("Transactions_SpendingID_FK");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Transactions_UserID_FK");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_PK");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053440DF954F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Birthdate).HasColumnType("datetime");
            entity.Property(e => e.CurrentBalance)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PreviousBalance)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserPassword).HasMaxLength(500);
            entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_UserRoleId_FK");
        });

        modelBuilder.Entity<VwSpendingCategoriesCount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwSpendingCategoriesCount");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.SpendingCategoryId).HasColumnName("SpendingCategoryID");
        });

        modelBuilder.Entity<VwUpcomingBirthday>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwUpcomingBirthdays");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
