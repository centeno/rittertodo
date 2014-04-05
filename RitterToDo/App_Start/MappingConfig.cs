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
                .AddMapping<ToDo, ToDoEditViewModel>()
                .From(f => f.OwnerId)
                .To(t => t.OwnerId);
        }
    }
}