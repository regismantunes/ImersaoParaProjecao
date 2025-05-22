using ImmersionToProjection.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ImmersionToProjection.Service.Configuration;

public class AppConfiguration : IAppConfiguration
{
    private string _filePath;
    private bool _loading = true;

    public AppConfiguration(string filePath)
    {
        _filePath = filePath;

        var json = File.ReadAllText(filePath);
        var configuration = JsonSerializer.Deserialize<ConfigurationTemplate>(json);

        if (configuration == null)
            throw new FileNotFoundException($"Configuration file not found at {filePath}");

        Language = configuration.Language;
        Theme = configuration.Theme;
        Patterns = configuration.Patterns;
        _loading = false;
    }

    public string Language
    { 
        get;
        set { field = value; UpdateSetting(); }
    }

    public string Theme
    {
        get;
        set { field = value; UpdateSetting(); }
    }

    public IEnumerable<PatternsItem> Patterns
    {
        get;
        set { field = value; UpdateSetting(); }
    }

    private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
        WriteIndented = true
    };

    public void Save()
    {
        var configuration = new ConfigurationTemplate
        {
            Language = Language,
            Theme = Theme,
            Patterns = Patterns
        };

        var json = JsonSerializer.Serialize<IConfigurationTemplate>(configuration, JsonSerializerOptions);
        File.WriteAllText(_filePath, json);
    }

    private void UpdateSetting([CallerMemberName] string? key = null)
    {
        if(_loading)
            return;

        ArgumentNullException.ThrowIfNull(key, nameof(key));

        OnPropertyChanged(key);

        Save();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}