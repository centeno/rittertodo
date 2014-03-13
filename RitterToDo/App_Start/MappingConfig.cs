using Moo;
using RitterToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RitterToDo.App_Start
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            MappingRepository.Default
                .AddMapping<ToDo, ToDoViewModel>()
                    .From(p => p.DueDate ?? DateTime.Now)
                    .To(p => p.DueDate);
        }
    }
}