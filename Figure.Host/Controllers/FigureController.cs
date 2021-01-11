using System;
using Figure.Contracts;
using Figure.Contracts.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace FigureApp.Controllers
{
    [ApiController]
    [Route("figure")]
    public class FigureController : ControllerBase
    {
        private readonly ILogger<FigureController> _logger;
        public IFigureService FigureService { get; }

        public FigureController(ILogger<FigureController> logger, IFigureService figureService)
        {
            _logger = logger;
            FigureService = figureService;
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateFigure([FromBody] FigureRequest figure)
        {
            var figureId = await FigureService.SaveFigureAsync(figure);
            return Ok(figureId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFigure(int id)
        {
            var square = await FigureService.GetFigureSquareAsync(id);
            return Ok(square);
        }
    }
}
