# Intro
While our company searched for some ways to implement localization for possible future software, i stumbled across the `MarkupExtension` found within a regular WPF application.

Upon reading a few articles, i decided to create a very simple Markup-Extension for localization


# Preparation in Code
```csharp
string languageTag = "de-DE";
string filePath = Path.Combine(Environment.CurrentDirectory, "lang", $"{languageTag}.json");
string json = File.ReadAllText(filePath);

// Static variable that provides an ILocalizer
SimpleLocalization.Localization.Localizer.Set(new JsonLocalizer(json, languageTag));

// Example
var appTitle = SimpleLocalization.Localization.Localizer["Common.AppTitle"];
```
Language-Json:
```json
{
    "Common": {
        "AppTitle":"MyApp"
    },
    "Greeting":"Hello"
}
```
# Usage inside Xaml
```xml
<Window x:Class="SimpleLocalization.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d"

        xmlns:l="clr-namespace:SimpleLocalization.Localization.Xaml"
        Title="{l:Loc Common.WindowTitle}" Height="350" Width="525">
</Window>
```
