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
    public class ToDoControllerTests
    {
        private ToDoController CreateSUT()
        {
            return new ToDoController(
                A.Fake<IRepository<ToDo>>(),
                A.Fake<IMappingRepository>());
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
            var mapperMock = A.Fake<IExtensibleMapper<ToDo, ToDoViewModel>>();
            A.CallTo(() => sut.MappingRepository.ResolveMapper<ToDo, ToDoViewModel>()).Returns(mapperMock);
            A.CallTo(() => mapperMock.MapMultiple(entities))
                .Returns(models);

            var result = sut.Index();

            var vr = result.ShouldBeViewResult();
            vr.Model.ShouldBeSameAs(models);
        }

        [Test]
        public void Update_GetById_PopulatesView()
        {
            var sut = CreateSUT();
            var fixture = new Fixture();
            var model = fixture.Create<ToDoEditViewModel>();
            var entity = fixture.Create<ToDo>();
            var id = Guid.NewGuid();
            A.CallTo(() => sut.ToDoRepo.GetById(id))
                .Returns(entity);
            var mapperMock = A.Fake<IExtensibleMapper<ToDo, ToDoEditViewModel>>();
            A.CallTo(() => sut.MappingRepository.ResolveMapper<ToDo, ToDoEditViewModel>()).Returns(mapperMock);
            A.CallTo(() => mapperMock.Map(entity))
                .Returns(model);

            var result = sut.Update(id);

            var vr = result.ShouldBeViewResult();
            vr.Model.ShouldBeSameAs(model);
        }
    }
}
