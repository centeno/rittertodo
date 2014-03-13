using FakeItEasy;
using Moo;
using NUnit.Framework;
using RitterToDo.Controllers;
using RitterToDo.Models;
using RitterToDo.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RitterToDo.Tests.TestHelpers;
using Ploeh.AutoFixture;
using Should;

namespace RitterToDo.Tests.Controllers
{
    [TestFixture]
    public class ToDoControllerTest
    {
        private ToDoController CreateSUT()
        {
            return new ToDoController(
                A.Fake<IRepository<ToDo>>(),
                A.Fake<IMapper<ToDo, ToDoViewModel>>());
        }

        [Test]
        public void Index_DefaultCase_ShowsToDoList()
        {
            var sut = CreateSUT();
            var fixture = new Fixture();
            var entities = fixture.CreateMany<ToDo>();
            var models = fixture.CreateMany<ToDoViewModel>();
            A.CallTo(() => sut.ToDoRepo.GetAll())
                .Returns(entities);
            A.CallTo(() => sut.EntityMapper.MapMultiple(entities))
                .Returns(models);

            var result = sut.Index();

            var vr = result.ShouldBeViewResult();
            vr.Model.ShouldBeSameAs(models);
        }
    }
}
