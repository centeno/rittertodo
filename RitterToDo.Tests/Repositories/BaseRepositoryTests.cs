using FakeItEasy;
using NUnit.Framework;
using Ploeh.AutoFixture;
using RitterToDo.Core;
using RitterToDo.Models;
using RitterToDo.Repos;
using RitterToDo.Tests.TestHelpers;
using Should;
using System;
using System.Linq;

namespace RitterToDo.Tests.Repositories
{
    [TestFixture(TypeArgs = new[] { typeof(ToDo) })]
    [TestFixture(TypeArgs = new[] { typeof(ToDoCategory) })]
    public class BaseRepositoryTests<T> where T : class, IOwnedEntity
    {
        private BaseRepository<T> CreateSUT()
        {
            return new BaseRepository<T>(
                A.Fake<IIdentityHelper>(),
                A.Fake<IApplicationDbContext>());
        }

        [Test]
        public void GetAll_DefaultCase_FetchesFromDbContext()
        {
            var sut = CreateSUT();
            var id = "1";
            var fixture = new Fixture();
            var rawData = fixture.CreateMany<T>(10).ToArray();
            var filteredData = new T[] { rawData[2], rawData[6], rawData[7] };
            foreach (var d in filteredData) { d.OwnerId = id; }
            var fakeDbSet = new InMemoryDbSet<T>(rawData);

            A.CallTo(() => sut.IdHelper.GetUserId()).Returns(id);
            A.CallTo(() => sut.DbContext.GetEntitySet<T>()).Returns(fakeDbSet);

            var result = sut.GetAll();
            CollectionAssert.AreEquivalent(result, filteredData);
        }

        [Test]
        public void GetByOwner_DefaultCase_FetchesFromDbContext()
        {
            var sut = CreateSUT();
            var id = Guid.NewGuid().ToString();
            var fixture = new Fixture();
            var rawData = fixture.CreateMany<T>(10).ToArray();
            var filteredData = new T[] { rawData[2], rawData[6], rawData[7] };
            foreach (var d in filteredData) { d.OwnerId = id; }
            var fakeDbSet = new InMemoryDbSet<T>(rawData);

            A.CallTo(() => sut.DbContext.GetEntitySet<T>()).Returns(fakeDbSet);

            var result = sut.GetByOwner(id);

            CollectionAssert.AreEquivalent(result, filteredData);
        }

        [Test]
        public void GetById_DefaultCase_FetchesFromDbContext()
        {
            var sut = CreateSUT();
            var id = Guid.NewGuid();
            var fixture = new Fixture();
            var entity = fixture.Create<T>();
            A.CallTo(() => sut.DbContext.GetById<T>(id)).Returns(entity);

            var result = sut.GetById(id);

            result.ShouldBeSameAs(entity);
        }

        [Test]
        public void Update_DefaultCase_RedirectsToDbContext()
        {
            var sut = CreateSUT();
            var entity = new Fixture().Create<T>();

            sut.Update(entity);

            A.CallTo(() => sut.DbContext.Update(entity)).MustHaveHappened();
        }
    }
}
