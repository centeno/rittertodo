using RitterToDo.Core;
using RitterToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moo;
using Moo.Extenders;
using Machine.Specifications;

namespace RitterToDo.IntegratedTests.TestHelpers
{
    public static class DataHelper
    {
        internal static readonly string DefaultUserId = "320840b0-78d6-4aed-9912-65c1cd180990";

        private static ApplicationUser dummyUsr;

        public static IEnumerable<ToDo> DummyToDos { get; private set; }

        public static IEnumerable<ToDoCategory> DummyCategories { get; private set; }

        public static IEnumerable<ToDoViewModel> DummyToDoViewModels { get; private set; }

        public static IEnumerable<ToDoCategoryViewModel> DummyCategoryViewModels { get; private set; }

        static DataHelper()
        {
            dummyUsr = new ApplicationUser() { Id = DefaultUserId };
            DummyToDos = new DummyDataGenerator().CreateDummyToDos(dummyUsr);
            DummyCategories = DummyToDos.Select(d => d.Category).Distinct();
            DummyToDoViewModels = DummyToDos.MapAll<ToDo, ToDoViewModel>();
            DummyCategoryViewModels = DummyCategories.MapAll<ToDoCategory, ToDoCategoryViewModel>();
        }
    }
}
