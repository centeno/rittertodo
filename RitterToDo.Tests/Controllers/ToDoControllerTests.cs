using System.Web.Mvc;
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
                A.Fake<IRepository<ToDoCategory>>(),
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
        public void Edit_GetById_PopulatesView()
        {
            // * Arrange
            //   - Preparing data and mocks
            var sut = CreateSUT();
            var fixture = new Fixture();
            var model = fixture.Create<ToDoEditViewModel>();
            var categories = fixture.CreateMany<ToDoCategory>();
            var catModels = fixture.CreateMany<ToDoCategoryViewModel>();
            var entity = fixture.Create<ToDo>();
            var id = Guid.NewGuid();
            var mapperMock = A.Fake<IExtensibleMapper<ToDo, ToDoEditViewModel>>();
            var categoryMapperMock = A.Fake<IExtensibleMapper<ToDoCategory, ToDoCategoryViewModel>>();
            //   - Setting up expectations
            A.CallTo(() => sut.MappingRepository.ResolveMapper<ToDo, ToDoEditViewModel>()).Returns(mapperMock);
            A.CallTo(() => sut.MappingRepository.ResolveMapper<ToDoCategory, ToDoCategoryViewModel>()).Returns(categoryMapperMock);
            A.CallTo(() => sut.ToDoRepo.GetById(id))
                .Returns(entity);
            A.CallTo(() => mapperMock.Map(entity))
                .Returns(model);
            A.CallTo(() => sut.TodoCategoryRepo.GetAll())
                .Returns(categories);
            A.CallTo(() => categoryMapperMock.MapMultiple(categories)).Returns(catModels);

            // * Act
            var result = sut.Edit(id);

            // * Assert
            //   - SUT should return a ViewModelResult
            var vr = result.ShouldBeViewResult();
            //   - SUT should return an entity object mapped into a ViewModel
            vr.Model.ShouldBeSameAs(model);
            //   - SUT should populate the ViewData attribute with category data
            vr.ViewData["Categories"].ShouldBeSameAs(catModels);
        }

        [Test]
        public void Edit_PostViewModel_SendToRepo()
        {
            var sut = CreateSUT();
            var fixture = new Fixture();
            var model = fixture.Create<ToDoEditViewModel>();
            var entity = fixture.Create<ToDo>();
            var mapperMock = A.Fake<IExtensibleMapper<ToDoEditViewModel, ToDo>>();

            A.CallTo(() => sut.MappingRepository.ResolveMapper<ToDoEditViewModel, ToDo>())
                .Returns(mapperMock);
            A.CallTo(() => mapperMock.Map(model)).Returns(entity);

            var result = sut.Edit(model);

            A.CallTo(() => sut.ToDoRepo.Update(entity)).MustHaveHappened();
            result.ShouldBeType<RedirectToRouteResult>();
        }

        [Test]
        public void Details_GetById_ShowsToDoDetails()
        {
            var sut = CreateSUT();
            var fixture = new Fixture();
            var model = fixture.Create<ToDoViewModel>();
            var entity = fixture.Create<ToDo>();
            var id = Guid.NewGuid();
            var mapperMock = A.Fake<IExtensibleMapper<ToDo, ToDoViewModel>>();
            
            A.CallTo(() => sut.ToDoRepo.GetById(id)).Returns(entity);
            A.CallTo(() => sut.MappingRepository.ResolveMapper<ToDo, ToDoViewModel>()).Returns(mapperMock);
            A.CallTo(() => mapperMock.Map(entity)).Returns(model);

            var result = sut.Details(id);

            var vr = result.ShouldBeViewResult();
            vr.Model.ShouldBeSameAs(model);
        }
    }
}
