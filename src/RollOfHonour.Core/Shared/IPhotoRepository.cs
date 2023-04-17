namespace RollOfHonour.Core.Shared;

public interface IPhotoRepository
{
  Task<IEnumerable<Models.Photo>> PersonPhotos(int personId);
  Task<IEnumerable<Models.Photo>> MemorialPhotos(int memorialId);
}
