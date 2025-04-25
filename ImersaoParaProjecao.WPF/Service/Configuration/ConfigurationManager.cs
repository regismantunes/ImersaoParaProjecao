using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ImmersionToProjection.Service.Configuration;

public class ConfigurationManager(IConfiguration configuration, string filePath) : IConfigurationManager
{
    public string? this[string key]
    {
        get => GetSetting(key);
        set => UpdateSetting(key, value);
    }

    public string GetSetting(string key)
    {
        return configuration[key] ?? string.Empty;
    }

    public void UpdateSetting(string key, string? value)
    {
        var json = File.ReadAllText(filePath);
        var jsonObj = JsonNode.Parse(json) as JsonObject;

        if (jsonObj == null)
            throw new InvalidDataException("The file is not a valid JSON.");

        var section = jsonObj;
        var keys = key.Split(':');
        for (int i = 0; i < keys.Length - 1; i++)
        {
            section = section[keys[i]] as JsonObject;
            if (section == null)
                throw new KeyNotFoundException($"The key '{keys[i]}' was not found.");
        }
        section[keys[^1]] = value;

        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, jsonObj.ToJsonString(options));

        configuration[key] = value;
        OnPropertyChanged(key);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public IConfigurationSection GetSection(string key)
    {
        return configuration.GetSection(key);
    }

    public IEnumerable<IConfigurationSection> GetChildren()
    {
        return configuration.GetChildren();
    }

    public IChangeToken GetReloadToken()
    {
        return configuration.GetReloadToken();
    }
}