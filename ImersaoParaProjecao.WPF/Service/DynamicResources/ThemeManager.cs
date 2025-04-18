using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;

namespace ImmersionToProjection.Service.DynamicResources;

public class ThemeManager : IThemeManager
{
    public void ApplyTheme(string? theme)
    {
        if (theme is null)
            theme = "Light";

        string resourcePath = $"/Resources/Styles/{theme}Style.xaml";

        ResourceDictionary newTheme = new ResourceDictionary
        {
            Source = new Uri(resourcePath, UriKind.RelativeOrAbsolute)
        };

        var mergedDictionaries = Application.Current.Resources.MergedDictionaries;

        ResourceDictionary? currentTheme = null;
        foreach (var dictionary in mergedDictionaries)
        {
            if (dictionary.Source != null &&
                dictionary.Source.OriginalString.StartsWith("/Resources/Styles/") &&
                !dictionary.Source.OriginalString.EndsWith("BaseStyle.xaml"))
            {
                currentTheme = dictionary;
                break;
            }
        }

        if (currentTheme != null)
            mergedDictionaries.Remove(currentTheme);

        mergedDictionaries.Add(newTheme);
    }
}
