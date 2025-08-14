public class AlarmProfile
{
    public int BatteryThreshold { get; set; }
    public string AudioFilePath { get; set; }
    public string CustomMessage { get; set; }
    public string ProfileName { get; set; }
}

public class UserSettings
{
    public string LastProfile { get; set; }
    public Dictionary<string, AlarmProfile> Profiles { get; set; } = new();
}
