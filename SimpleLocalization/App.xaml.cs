using SimpleLocalization.Localization;
using System;
using System.IO;
using System.Windows;

namespace SimpleLocalization
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string languageTag = "de-DE";
            var fi = new FileInfo(Path.Combine(Environment.CurrentDirectory, "lang", $"{languageTag}.json"));
            string json = "";
            if (fi.Exists)
            {
                using (var stream = fi.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }

            Localizer.Set(new JsonLocalizer(json, languageTag));

            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
    }
}
