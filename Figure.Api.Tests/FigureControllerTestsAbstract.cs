using FigureApp.Controllers;
using Moq;
using Figure.Business;
using Figure.SqliteDb;
using Figure.Contracts.Db;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Threading;

namespace Figure.Api.Tests
{
    public abstract class FigureControllerTestsAbstract
    {
        protected List<FigureRecord> _records = new List<FigureRecord>();
        protected int _currentId = 0;
        protected FigureController _controller;
        protected FigureControllerTestsAbstract() 
        {
            var figureRecord = GetQueryableMockDbSet(_records, x => x.Id = _currentId);
            var dbContextMock = new Mock<IDatabaseContext>();

            dbContextMock.SetupGet(x => x.Figures).Returns(figureRecord);

            var figureRepository = new FigureRepository(dbContextMock.Object);
            var figureService = new FigureService(figureRepository);

            _controller = new FigureController(figureService);
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList, Action<T> onAddAcction) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>())).Returns(new TestAsyncEnumerator<T>(queryable.GetEnumerator()));
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(queryable.Provider));
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) =>
            {
                onAddAcction(s);
                sourceList.Add(s);
            });

            return dbSet.Object;
        }

    }
}
