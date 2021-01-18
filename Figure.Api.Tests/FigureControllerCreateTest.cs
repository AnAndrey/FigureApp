using Xunit;
using FluentAssertions;
using Moq;
using Figure.Contracts;
using Figure.Business;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Figure.Api.Tests
{

    public class FigureControllerCreateTest:FigureControllerTestsAbstract
    {

        [Fact]
        public async Task CreateCircle()
        {
            //arrange
            _currentId++;

            var request = new FigureRequest() { Type = FigureType.Circle.ToString(), Params = new Dictionary<string, double> { { nameof(Circle.Radius), 2 } } };

            //act
            var actionResult = await _controller.CreateFigureAsync(request);
         
            //assert
            CheckCreatedFigureResponce(actionResult, request);
        }

        [Fact]
        public async Task CreateTriangle()
        {
            //arrange
            _currentId++;

            var request = new FigureRequest() 
            { 
                Type = FigureType.Triangle.ToString(), 
                Params = new Dictionary<string, double> { 
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
    }
}
