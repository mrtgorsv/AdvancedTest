using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using AdvancedTest.Data.Model;

namespace AdvancedTest.Data.Context
{
    
    /// <summary>
    /// Контекст базы данных приложения
    /// </summary>
    public class AppDbContext : DbContext
    {
        // Коллекция пользователей
        public DbSet<User> Users { get; set; }
        // Коллекция глав теории
        public DbSet<TheoryPart> TheoryParts { get; set; }
        // Коллекция разделов
        public DbSet<TheorySection> TheorySections { get; set; }
        // Коллекция заданий тестов
        public DbSet<TheoryTestPart> TheoryTestParts { get; set; }
        // Коллекция документов для глав
        public DbSet<TheoryDocument> TheoryDocuments { get; set; }

        public DbSet<TheoryTestPartAnswer> TheoryTestPartAnswers { get; set; }
        // Коллекция отметок о прохождении тестов пользователями
        public DbSet<UserTheoryTestMark> UserTheoryTestMarks { get; set; }
        // Коллекция отметок о просмотре документов глав
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
            HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class AnswerMap : EntityTypeConfiguration<TheoryTestPartAnswer>
    {
        public AnswerMap()
        {
            HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class UserTestMap : EntityTypeConfiguration<UserTheoryTestMark>
    {
        public UserTestMap()
        {
            HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class TestPartMap : EntityTypeConfiguration<TheoryTestPart>
    {
        public TestPartMap()
        {
            HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class PartDocumentMap : EntityTypeConfiguration<TheoryDocument>
    {
        public PartDocumentMap()
        {
            HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    #endregion
}