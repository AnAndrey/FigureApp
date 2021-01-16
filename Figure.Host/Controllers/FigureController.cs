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
        public IFigureService FigureService { get; }

        public FigureController(IFigureService figureService)
        {
            FigureService = figureService;
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateFigureAsync([FromBody] FigureRequest figure)
        {
            var figureResponse = await FigureService.SaveFigureAsync(figure);
            return Ok(figureResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFigureAsync(int id)
        {
            var areaResponse = await FigureService.GetFigureAreaAsync(id);
            return Ok(areaResponse);
        }
    }
}
