using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using AdvancedTest.Data.Model;

namespace AdvancedTest.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TheoryPart> TheoryParts { get; set; }
        public DbSet<TheoryTestPart> TheoryTestParts { get; set; }
        public DbSet<TheoryDocument> TheoryDocuments { get; set; }
        public DbSet<TheoryTestPartAnswer> TheoryTestPartAnswers { get; set; }
        public DbSet<UserTheoryTestMark> UserTheoryTestMarks { get; set; }
        public DbSet<UserTheoryDocumentMark> UserTheoryDocumentMarks { get; set; }

        public AppDbContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
//            modelBuilder.Configurations.Add(new UserMap());
//            modelBuilder.Configurations.Add(new PartMap());
//            modelBuilder.Configurations.Add(new TestMap());
//            modelBuilder.Configurations.Add(new AnswerMap());
//            modelBuilder.Configurations.Add(new UserTestMap());
//            modelBuilder.Configurations.Add(new TestPartMap());
//            modelBuilder.Configurations.Add(new PartDocumentMap());
        }
    }

    #region Entity Configuration Maps

    public class PartMap : EntityTypeConfiguration<TheoryPart>
    {
        public PartMap()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class AnswerMap : EntityTypeConfiguration<TheoryTestPartAnswer>
    {
        public AnswerMap()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class UserTestMap : EntityTypeConfiguration<UserTheoryTestMark>
    {
        public UserTestMap()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class TestPartMap : EntityTypeConfiguration<TheoryTestPart>
    {
        public TestPartMap()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class PartDocumentMap : EntityTypeConfiguration<TheoryDocument>
    {
        public PartDocumentMap()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    #endregion
}