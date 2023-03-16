namespace RollOfHonour.Data.Models.DB;

public partial class Decoration
{
    public Core.Models.Decoration ToDomainModel()
    {
        var person = new Core.Models.Decoration()
        {
            Id = this.Id,
            Name = this.Name is null ? string.Empty : this.Name,
            Initials = this.Initials is null ? string.Empty : this.Initials,
            Description = this.Description is null ? string.Empty : this.Description
        };

        return person;
    }

    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Initials { get; set; }

    public virtual ICollection<Person> People { get; } = new List<Person>();

    public virtual ICollection<RecordedName> RecordedNames { get; } = new List<RecordedName>();
}
