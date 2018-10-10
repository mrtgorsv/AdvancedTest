using System.IO;

namespace AdvancedTest.Data.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static string GetEmbeddedSqlMigration(this string migration, string fileName)
        {
            if (!Path.HasExtension(fileName))
            {
                fileName = Path.ChangeExtension(fileName, ".sql");
            }

            return typeof(EntityFrameworkExtensions).Assembly.GetEmbeddedFile($"Scripts._{migration}.{fileName}");
        }
    }
}