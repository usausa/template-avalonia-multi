namespace Template.DesktopApp.Settings;

using System.Text.Json;

public sealed class UserSettingStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new() { WriteIndented = true };

    private readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Template.DesktopApp", "usersetting.json");

    public UserSetting Value { get; private set; } = new();

    public void Load()
    {
        try
        {
            if (File.Exists(path))
            {
                Value = JsonSerializer.Deserialize<UserSetting>(File.ReadAllText(path), SerializerOptions) ?? new UserSetting();
            }
        }
        catch (JsonException)
        {
            Value = new UserSetting();
        }
        catch (IOException)
        {
            Value = new UserSetting();
        }
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, JsonSerializer.Serialize(Value, SerializerOptions));
        }
        catch (IOException)
        {
        }
    }
}
