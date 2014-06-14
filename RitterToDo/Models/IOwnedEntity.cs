namespace RitterToDo.Models
{
    public interface IOwnedEntity : IEntity
    {
        ApplicationUser Owner { get; set; }

        string OwnerId { get; set; }
    }
}