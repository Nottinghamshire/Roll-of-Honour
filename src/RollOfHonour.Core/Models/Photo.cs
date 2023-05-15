namespace RollOfHonour.Core.Models;

public class Photo
{
    private string _imageUrlPrefix;

    public Photo(string imageUrlPrefix)
    {
        _imageUrlPrefix = imageUrlPrefix;
    }

    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;

    public Uri ImageUriOriginal => new Uri($"{_imageUrlPrefix}/{Id}/original.jpg");
    public Uri ImageUriTiny => new Uri($"{_imageUrlPrefix}/{Id}/tiny.jpg");
    public Uri ImageUriThumbnail => new Uri($"{_imageUrlPrefix}/{Id}/thumbnail.jpg");
    public Uri ImageUriSmall => new Uri($"{_imageUrlPrefix}/{Id}/small.jpg");
    public Uri ImageUriMedium => new Uri($"{_imageUrlPrefix}/{Id}/medium.jpg");
    public Uri ImageUriLarge => new Uri($"{_imageUrlPrefix}/{Id}/large.jpg");
}
