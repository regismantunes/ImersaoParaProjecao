using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ImmersionToProjection.Service.Configuration;

public class ConfigurationUpdater(string filePath) : IConfigurationUpdater
{
    public void UpdateSetting(string key, string value)
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
    }
}