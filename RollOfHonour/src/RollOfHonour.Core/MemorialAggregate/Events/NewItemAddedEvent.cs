using RollOfHonour.Core.ProjectAggregate;
using RollOfHonour.SharedKernel;

namespace RollOfHonour.Core.MemorialAggregate.Events;

public class NewRecordedNameAddedEvent : DomainEventBase
{
  public RecordedName NewName { get; set; }
  public Memorial Memorial { get; set; }

  public NewRecordedNameAddedEvent(Memorial memorial,
      RecordedName newName)
  {
    Memorial = memorial;
    NewName = newName;
  }
}
