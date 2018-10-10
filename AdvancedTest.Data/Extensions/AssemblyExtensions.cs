using System.IO;
using System.Reflection;

namespace AdvancedTest.Data.Extensions
{
    internal static class AssemblyExtensions
    {
        public static string GetEmbeddedFile(this Assembly assembly, string filePath)
        {
            var name = $"{assembly.GetName().Name}.{filePath}";

            using (var stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                {
                    return null;
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            };
        }
    }
}
