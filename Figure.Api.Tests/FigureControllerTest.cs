using Xunit;
using FluentAssertions;
using FigureApp.Controllers;
using Moq;
using Figure.Contracts;
using Figure.Business;
using Figure.SqliteDb;
using Figure.Contracts.Db;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Figure.Api.Tests
{
    public class FigureControllerTest
    {
        List<FigureRecord> _records = new List<FigureRecord>();
        int _currentId = 0;
        FigureController _controller;
        public FigureControllerTest() 
        {
            var figureRecord = GetQueryableMockDbSet(_records, x => x.Id = _currentId);
            var dbContextMock = new Mock<IDatabaseContext>();

            dbContextMock.SetupGet(x => x.Figures).Returns(figureRecord);

            var figureRepository = new FigureRepository(dbContextMock.Object);
            var figureService = new FigureService(figureRepository);

            _controller = new FigureController(figureService);
        }

        [Fact]
        public async Task CheckMarketCreatedTest()
        {
            //arrange
            _currentId++;

            var request = new FigureRequest() { Type = FigureType.Circle.ToString(), Params = new Dictionary<string, int> { { nameof(Circle.Radius), 2 } } };

            //act
            var actionResult = await _controller.CreateFigureAsync(request);
         
            //assert
            CheckCreatedFigureResponce(actionResult, request);
        }

        [Fact]
        public async Task CheckMarketCreatedTest2()
        {
            //arrange
            _currentId++;

            var request = new FigureRequest() 
            { 
                Type = FigureType.Triangle.ToString(), 
                Params = new Dictionary<string, int> { 
                    { nameof(Triangle.SideA), 18 },
                    { nameof(Triangle.SideB), 30 },
                    { nameof(Triangle.SideC), 24 }
                } 
            };

            //act
            var actionResult = await _controller.CreateFigureAsync(request);

            //assert
            CheckCreatedFigureResponce(actionResult, request);
        }

        private void CheckCreatedFigureResponce(IActionResult actionResult, FigureRequest request) 
        {
            var response = actionResult.Should().BeOfType<OkObjectResult>()
            .Subject.Value.Should().BeOfType<CreatedFigureResponce>()
            .Subject;

            response.Should().NotBeNull();
            response.Type.Should().Be(request.Type);
            response.Id.Should().Be(_currentId);
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList, Action<T> onAddAcction) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
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
