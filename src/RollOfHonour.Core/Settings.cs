namespace RollOfHonour.Core;

public class AppSettings
{
    public Storage Storage { get; set; } = null!;
}

public class Storage
{
    public string ImageUrlPrefix { get; set; } = string.Empty;
}
