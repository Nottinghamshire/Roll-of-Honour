using RollOfHonour.Core.ProjectAggregate;
using RollOfHonour.SharedKernel;

namespace RollOfHonour.Core.MemorialAggregate.Events;

public class PersonMatchedToRecordedNameEvent : DomainEventBase
{
  public int PersonId { get; set; }
  public RecordedName RecordedName { get; set; }

  public PersonMatchedToRecordedNameEvent(RecordedName name, int personId)
  {
    RecordedName = name;
    PersonId = personId;
  }
}
