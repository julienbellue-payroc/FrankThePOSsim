using System;
using System.IO;
using System.Text.Json;

namespace FrankThePOSsim.Helpers;

public class ConfigSaverHelper
{
    public static void SaveToFile(Config config)
    {
        var jsonWriteOptions = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        var configurationWrapper = new
        {
            Config = config
        };
        
        var newJson = JsonSerializer.Serialize(configurationWrapper, jsonWriteOptions);
        var appSettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, App.ConfigFilePath);
        File.WriteAllText(appSettingsPath, newJson);
    }
}
