using System;
using System.IO;
using Allure.Commons;

namespace UiTests.Support
{
    public static class AllureHelper
    {
        private static readonly AllureLifecycle Life = AllureLifecycle.Instance;

        public static void AddTextAttachment(string name, string content)
        {
            var file = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");
            File.WriteAllText(file, content);
            Life.AddAttachment(name, "text/plain", file);
        }

        public static string SaveTempFile(byte[] bytes, string ext)
        {
            var file = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ext);
            File.WriteAllBytes(file, bytes);
            return file;
        }

        public static void AddScreenshot(string name, byte[] bytes)
        {
            var file = SaveTempFile(bytes, ".png");
            Life.AddAttachment(name, "image/png", file);
        }
    }
}