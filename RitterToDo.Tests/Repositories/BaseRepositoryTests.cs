using FakeItEasy;
using NUnit.Framework;
using Ploeh.AutoFixture;
using RitterToDo.Core;
using RitterToDo.Models;
using RitterToDo.Repos;
using RitterToDo.Tests.TestHelpers;
using Should;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitterToDo.Tests.Repositories
{
    [TestFixture(TypeArgs=new [] { typeof(ToDo) })]
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
    }
}
