using System.Web.Mvc;
using FakeItEasy;
using Moo;
using NUnit.Framework;
using RitterToDo.Controllers;
using RitterToDo.Core;
using RitterToDo.Models;
using RitterToDo.Repos;
using System;
using RitterToDo.Tests.TestHelpers;
using Ploeh.AutoFixture;
using Should;
using System.Linq;
using System.Collections.Generic;

namespace RitterToDo.Tests.Controllers
{
    [TestFixture]
    public class ToDoControllerTests
    {
        private ToDoController CreateSUT()
        {
            return new ToDoController(
                A.Fake<IRepository<ToDo>>(),
                A.Fake<ILookupHelper<ToDoCategory, ToDoCategoryViewModel>>(),
                A.Fake<IMappingRepository>());
        }

        [TestCaseSource("GetStarredCases")]
        public void GetStarred_DefaultCase_ReturnsStarredToDos(IEnumerable<ToDo> entities)
        {
            var sut = CreateSUT();
            var mapperMock = A.Fake<IExtensibleMapper<ToDo, ToDoViewModel>>();
            A.CallTo(() => sut.ToDoRepo.GetAll()).Returns(entities);
            A.CallTo(() => sut.MappingRepository.ResolveMapper<ToDo, ToDoViewModel>()).Returns(mapperMock);
            A.CallTo(() => mapperMock.MapMultiple(
                A<IEnumerable<ToDo>>.That.Matches(
                    list => (list.All(i => i.Starred)) 
                        && (list.Count() == entities.Count(e => e.Starred))
                )))
                .Returns(new ToDoViewModel[0]);

            var result = sut.GetStarred();

            var vr = result.ShouldBeViewResult();
            vr.Model.ShouldNotBeNull();
        }

        public IEnumerable<IEnumerable<ToDo>> GetStarredCases()
        {
            return new[]
            {
                new[]
                {
                    new ToDo() { Starred = true }, 
                    new ToDo() { Starred = false },
                    new ToDo() { Starred = true }, 
                    new ToDo() { Starred = false },
                }
                ,
                new[]
                {
                    new ToDo() { Starred = true }, 
                    new ToDo() { Starred = false },
                    new ToDo() { Starred = false },
                }
                ,
                new[]
                {
                    new ToDo() { Starred = true }, 
                    new ToDo() { Starred = false },
                }
                ,
                new[]
                {
                    new ToDo() { Starred = true }, 
                    new ToDo() { Starred = true },
                }
                ,
                new[]
                {
                    new ToDo() { Starred = false }, 
                    new ToDo() { Starred = false },
                }
                ,
                new[]
                {
                    new ToDo() { Starred = false }, 
                }
                ,
                new[]
                {
                    new ToDo() { Starred = true }, 
                }
                ,
                new ToDo[] {},
            };
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
            // * Arrange
            //   - Preparing data and mocks
            var sut = CreateSUT();
            var fixture = new Fixture();
            var model = fixture.Create<ToDoEditViewModel>();
            var catModels = fixture.CreateMany<ToDoCategoryViewModel>();
            var entity = fixture.Create<ToDo>();
            var id = Guid.NewGuid();
            var mapperMock = A.Fake<IExtensibleMapper<ToDo, ToDoEditViewModel>>();
            //   - Setting up expectations
            A.CallTo(() => sut.MappingRepository.ResolveMapper<ToDo, ToDoEditViewModel>()).Returns(mapperMock);
            A.CallTo(() => sut.ToDoRepo.GetById(id))
                .Returns(entity);
            A.CallTo(() => mapperMock.Map(entity))
                .Returns(model);
            A.CallTo(() => sut.CategoryHelper.GetAll()).Returns(catModels);

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
        public void Update_PostViewModel_SendToRepo()
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
    }
}
