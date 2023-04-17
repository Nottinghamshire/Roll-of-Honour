namespace RollOfHonour.Core.Models;

public class Photo
{
  private string _blobServiceName;
  private string _blobContainer;

  public Photo(string blobServiceName, string blobImagesContainer)
  {
    _blobServiceName = blobServiceName;
    _blobContainer = blobImagesContainer;
  }
  
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }

  public Uri ImageUriOriginal => new Uri($"https://{_blobServiceName}.blob.core.windows.net/{_blobContainer}/{Id}/original.jpg");  
  public Uri ImageUriTiny => new Uri($"https://{_blobServiceName}.blob.core.windows.net/{_blobContainer}/{Id}/tiny.jpg");  
  public Uri ImageUriThumbnail => new Uri($"https://{_blobServiceName}.blob.core.windows.net/{_blobContainer}/{Id}/thumbnail.jpg");  
  public Uri ImageUriSmall => new Uri($"https://{_blobServiceName}.blob.core.windows.net/{_blobContainer}/{Id}/small.jpg");  
  public Uri ImageUriMedium => new Uri($"https://{_blobServiceName}.blob.core.windows.net/{_blobContainer}/{Id}/medium.jpg");  
  public Uri ImageUriLarge => new Uri($"https://{_blobServiceName}.blob.core.windows.net/{_blobContainer}/{Id}/large.jpg");  

}
