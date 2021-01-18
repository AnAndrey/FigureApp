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
using Figure.Contracts.Exceptions;
using Xunit.Sdk;
using System.Reflection;

namespace Figure.Api.Tests
{

    public class FigureControllerValidationTests
    {

        [Theory]
        [InvalidFigureRequests]
        public async Task CreateFigureAsyncWithInvalidData_ShouldThrowException(FigureRequest request, Type expectedException)
        {
            //arrange
            var figureRepository = new FigureRepository(null);
            var figureService = new FigureService(figureRepository);
            var controller = new FigureController(figureService);

            //act & assert
            var exception = await Assert.ThrowsAsync(expectedException, async () => await controller.CreateFigureAsync(request));
        }

        private class InvalidFigureRequestsAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo testMethod)
            {
                yield return new object[] { new FigureRequest() { Type = FigureType.Circle.ToString(),
                    Params = new Dictionary<string, int> () }, typeof(InvalidFigureRequestException) };
                yield return new object[] { new FigureRequest() { Type = FigureType.Circle.ToString(),
                    Params = new Dictionary<string, int> { { nameof(Circle.Radius), -1 } } }, typeof(InvalidFigureException) };
                yield return new object[] { new FigureRequest() { Type = FigureType.Triangle.ToString(),
                    Params = new Dictionary<string, int> () }, typeof(InvalidFigureRequestException) };
                yield return new object[] { new FigureRequest() { Type = FigureType.Triangle.ToString(),
                    Params = new Dictionary<string, int> { 
                        { nameof(Triangle.SideA), 1 }, 
                        { nameof(Triangle.SideC), 4 }, 
                        { nameof(Triangle.SideB), 5 } } }, 
                    typeof(InvalidFigureException) };
                yield return new object[] { new FigureRequest() { Type = "InvalidFigureType",
                    Params = new Dictionary<string, int> {
                        { "Param", 1 }}},
                    typeof(InvalidFigureTypeException) };
            }
        }
    }
    

}
