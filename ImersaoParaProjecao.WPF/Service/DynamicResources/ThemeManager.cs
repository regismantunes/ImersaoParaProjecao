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

        var resourcePath = $"/Resources/Themes/{theme}.xaml";

        var newTheme = new ResourceDictionary
        {
            Source = new Uri(resourcePath, UriKind.Relative)
        };

        var mergedDictionaries = Application.Current.Resources.MergedDictionaries;

        ResourceDictionary? currentTheme = null;
        foreach (var dictionary in mergedDictionaries)
        {
            if (dictionary.Source != null &&
                dictionary.Source.OriginalString.StartsWith("/Resources/Themes/"))
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