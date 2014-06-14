using System;

namespace RitterToDo.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }

        string Name { get; set; }
    }
}
