using System.Text.Json;

namespace Common
{
    public static class FileHelper
    {
        public static void CreateFileIfNotExist(string path, string content)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, content);
                Console.WriteLine($"File created. Path: {path}");
            }
        }
    }
}
