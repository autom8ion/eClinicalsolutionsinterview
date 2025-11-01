
using System;
using System.IO;
using OpenQA.Selenium;

namespace UiTests.Utilities
{
    public static class ScreenshotHelper
    {
        public static string TakeScreenshot(IWebDriver driver, string name, string screenshotsDir)
        {
            Directory.CreateDirectory(screenshotsDir);
            var fileName = $"{Sanitize(name)}_{DateTime.UtcNow:yyyyMMdd_HHmmssfff}.png";
            var fullPath = Path.Combine(screenshotsDir, fileName);
            var shot = ((ITakesScreenshot)driver).GetScreenshot();
            File.WriteAllBytes(fullPath, shot.AsByteArray);
            return fullPath;
        }

        private static string Sanitize(string s)
            => string.Join("_", s.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
    }
}
