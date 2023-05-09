namespace RollOfHonour.Core;

public class AppSettings
{
    public Storage Storage { get; set; } = null!;
}
public class Storage
{
    public string BlobName { get; set; } = String.Empty;
    public string BlobImageContainerName { get; set; } = String.Empty;
}
