using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Person;

public class Details : PageModel
{
  public Core.Models.Person Person { get; set; }
  public Photo MainPhoto { get; set; }
  public List<Photo> Photos { get; set; } = new List<Photo>();

  private IPersonRepository _personRepository { get; set; } = null!;
  private IPhotoRepository _photoRepository { get; set; } = null!;

  public Details(IPersonRepository personRepository, IPhotoRepository photoRepository)
  {
    _personRepository = personRepository;
    _photoRepository = photoRepository;
  }

  public async Task<IActionResult> OnGet(int id)
  {
    var person = await _personRepository.GetById(id);

    if (person == null)
    {
      return NotFound();
    }

    //Get Photo IDs
    IEnumerable<Photo>
      otherPhotos =
        await _photoRepository
          .PersonPhotos(person
            .Id); //TODO: Could include in main person query? But a generic way might be nice for consistency

    //Photo decision
    //If main photo null, then use the first of the other images (or a random one)
    if (person.MainPhotoId == null && otherPhotos.Any())
    {
      //Promote an image to MainPhoto
      MainPhoto = otherPhotos.FirstOrDefault();
      foreach (var otherPhoto in otherPhotos.Where(x => MainPhoto != null && x.Id != MainPhoto.Id))
      {
        Photos.Add(otherPhoto);
      }
    }


    Person = person;
    return Page();
  }
}
