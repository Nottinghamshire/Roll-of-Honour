namespace RollOfHonour.Core;

public class AppSettings
{
    public Storage Storage { get; set; } = null!;
    public Whitelists Whitelists { get; set; } = null!;
    public APIBasicAuth APIConnectorsAuth { get; set; } = null!;
}

public class Storage
{
    public string ImageUrlPrefix { get; set; } = string.Empty;
}

public class Whitelists
{
    public string SignupEmails { get; set; } = string.Empty;
}

public class APIBasicAuth
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
