using RollOfHonour.Core.ContributorAggregate;
using RollOfHonour.Core.ProjectAggregate;
using RollOfHonour.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using RollOfHonour.Core.MemorialAggregate;

namespace RollOfHonour.Web;

public static class SeedData
{
  public static readonly RecordedName RecordedName1 = new RecordedName("C Frank");
  public static readonly RecordedName RecordedName2 = new RecordedName("Bob Test");
  public static readonly RecordedName RecordedName3 = new RecordedName("B Test");
  public static readonly Memorial TestMemorial1 = new Memorial("Test Memorial 1", new Point(91, 41));
  public static readonly Memorial TestMemorial2 = new Memorial("Test Memorial 2", "Lorem Ipsum", new Point(81, 35));
  public static readonly Person Person1 = new Person() { FirstNames = "Chris", LastName = "Frank" };
  public static readonly Person Person2 = new Person() { FirstNames = "Bob", LastName = "Test" };

  public static void Initialize(IServiceProvider serviceProvider)
  {
    using var dbContext = new AppDbContext(
      serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null);
    // Look for any TODO items.
    if (dbContext.People.Any())
    {
      return; // DB has been seeded
    }

    PopulateTestData(dbContext);
  }

  public static void PopulateTestData(AppDbContext dbContext)
  {
    foreach (var item in dbContext.Memorials)
    {
      dbContext.Remove(item);
    }

    foreach (var item in dbContext.People)
    {
      dbContext.Remove(item);
    }

    foreach (var item in dbContext.Names)
    {
      dbContext.Remove(item);
    }

    dbContext.SaveChanges();

    dbContext.People.Add(Person1);
    dbContext.People.Add(Person2);

    dbContext.SaveChanges();

    RecordedName1.MatchToPerson(Person1.Id);
    RecordedName2.MatchToPerson(Person2.Id);
    RecordedName3.MatchToPerson(Person2.Id);

    TestMemorial1.RecordName(RecordedName1);
    TestMemorial1.RecordName(RecordedName2);
    TestMemorial2.RecordName(RecordedName3);
    dbContext.Memorials.Add(TestMemorial1);

    dbContext.SaveChanges();
  }
}
