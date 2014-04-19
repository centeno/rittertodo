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
                new ToDo() { Category = generalCategory, ToDoCategoryId = generalCategory.Id, Name = "Hello World!", Description = "My first to-do!", Owner = appUser, OwnerId = appUser.Id, DueDate = DateTime.Now.AddMonths(1), Starred = true },
                new ToDo() { Category = personalCategory, ToDoCategoryId = personalCategory.Id, Name = "Supermarket list", Description = "Add your shopping list here", Owner = appUser, OwnerId = appUser.Id },
            };
        }
    }
}