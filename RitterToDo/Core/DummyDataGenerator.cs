using RitterToDo.Models;
using System;
using System.Collections.Generic;

namespace RitterToDo.Core
{
    public class DummyDataGenerator
    {
        public IEnumerable<ToDo> CreateDummyToDos(ApplicationUser appUser)
        {
            var generalCategory = new ToDoCategory() { Name = "General", Owner = appUser, OwnerId = appUser.Id };
            var personalCategory = new ToDoCategory() { Name = "Personal", Owner = appUser, OwnerId = appUser.Id };
            return new ToDo[]
            {
                new ToDo() 
                { 
                    Id = Guid.Parse("7af95b24-3a32-4816-ab8f-a903e5d758a3"), 
                    Category = generalCategory, 
                    ToDoCategoryId = generalCategory.Id, 
                    Name = "Hello World!", 
                    Description = "My first to-do!", 
                    Owner = appUser, 
                    OwnerId = appUser.Id, 
                    DueDate = DateTime.Now.AddMonths(1), 
                    Starred = true 
                },
                new ToDo() 
                { 
                    Id = Guid.Parse("602b9832-c1ca-493e-a3cd-65690c0cc742"), 
                    Category = personalCategory, 
                    ToDoCategoryId = personalCategory.Id, 
                    Name = "Supermarket list", 
                    Description = "Add your shopping list here", 
                    Owner = appUser, 
                    OwnerId = appUser.Id 
                },
            };
        }
    }
}