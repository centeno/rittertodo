using FakeItEasy;
using Moo;
using NUnit.Framework;
using Ploeh.AutoFixture;
using RitterToDo.Core;
using RitterToDo.Models;
using RitterToDo.Repos;
using Should;

namespace RitterToDo.Tests.Core
{
    [TestFixture(TypeArgs = new[] { typeof(ToDoCategory), typeof(ToDoCategoryViewModel) })]
    public class LookupHelperTests<TEntity, TModel>
        where TEntity : class, IOwnedEntity
    {
        [Test]
        public void GetAll_DefaultCase_FetchesAndMaps()
        {
            var sut = CreateSUT();
            var fixture = new Fixture();
            var entities = fixture.CreateMany<TEntity>();
            var models = fixture.CreateMany<TModel>();
            var mapperMock = A.Fake<IExtensibleMapper<TEntity, TModel>>();

            A.CallTo(() => sut.Repo.GetAll()).Returns(entities);
            A.CallTo(() => sut.MappingRepo.ResolveMapper<TEntity, TModel>()).Returns(mapperMock);
            A.CallTo(() => mapperMock.MapMultiple(entities)).Returns(models);

            var result = sut.GetAll();

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(models);
        }

        private LookupHelper<TEntity, TModel> CreateSUT()
        {
            return new LookupHelper<TEntity, TModel>(
                A.Fake<IRepository<TEntity>>(),
                A.Fake<IMappingRepository>());
        }
    }
}
