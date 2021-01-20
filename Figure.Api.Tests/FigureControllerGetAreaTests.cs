using Xunit;
using FluentAssertions;
using Figure.Contracts;
using Figure.Contracts.Db;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Figure.Business;
using Figure.Contracts.Exceptions;

namespace Figure.Api.Tests
{
    public class FigureControllerGetAreaTests: FigureControllerTestsAbstract
    { 
        [Fact]
        public async Task GetCircleArea() 
        {
            //arrange
            var id = ++_currentId;
            var record = new FigureRecord()
            {
                Id = id,
                Type = FigureType.Circle,
                Params = JsonConvert.SerializeObject(new Circle(1)),
            };
            _records.Add(record);

            //act
            var actionResult = await _controller.GetFigureAsync(id);

            //assert
            var response = actionResult.Should().BeOfType<OkObjectResult>()
                            .Subject.Value.Should().BeOfType<FigureAreaResponce>()
                            .Subject;

            response.Should().NotBeNull();
            response.Type.Should().Be(record.Type.ToString());
            response.Id.Should().Be(record.Id);
            response.Area.Should().Be(Math.PI);
        }

        [Fact]
        public async Task GetTriangleArea()
        {
            //arrange
            var id = ++_currentId;
            var record = new FigureRecord()
            {
                Id = id,
                Type = FigureType.Triangle,
                Params = JsonConvert.SerializeObject(new Triangle(8.8, 12.2, 18.0)),
            };
            _records.Add(record);

            //act
            var actionResult = await _controller.GetFigureAsync(id);

            //assert
            var response = actionResult.Should().BeOfType<OkObjectResult>()
                            .Subject.Value.Should().BeOfType<FigureAreaResponce>()
                            .Subject;

            response.Should().NotBeNull();
            response.Type.Should().Be(record.Type.ToString());
            response.Id.Should().Be(record.Id);
            response.Area.Should().Be(47.79871860207133);
        }

        [Fact]
        public async Task GetFigureAreaAsyncWithUnknownId_ShouldThrowNotFoundFigureException()
        {
            //arrange
            var id = _currentId+1;

            //act & assert
            await Assert.ThrowsAsync<NotFoundFigureException>(async () => await _controller.GetFigureAsync(id));
        }

        [Fact]
        public async Task GetFigureAreaAsyncWithInvalidType_ShouldThrowInvalidFigureTypeException()
        {
            //arrange
            var id = ++_currentId;
            var record = new FigureRecord()
            {
                Id = id,
                Type = FigureType.None
            };
            _records.Add(record);

            //act & assert
            await Assert.ThrowsAsync<InvalidFigureTypeException>(async () => await _controller.GetFigureAsync(id));
        }

        [Fact]
        public async Task GetFigureAreaAsyncWithInvalidParams_ShouldThrowCorruptedFigureException()
        {
            //arrange
            var id = _currentId;
            var record = new FigureRecord()
            {
                Id = id,
                Type = FigureType.Triangle,
                Params = JsonConvert.SerializeObject(new Triangle(1, 4, 5)),
            };
            _records.Add(record);

            //act & assert
            await Assert.ThrowsAsync<CorruptedFigureException>(async () => await _controller.GetFigureAsync(id));

        }

    }
}
