using Figure.Contracts;
using Figure.Contracts.Db;
using Figure.Contracts.Exceptions;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Figure.Business
{
    public class FigureService : IFigureService
    {
        private readonly IFigureRepository _repository;

        public FigureService(IFigureRepository repository)
        {
            _repository = repository;
        }
        public async Task<CreatedFigureResponce> SaveFigureAsync(FigureRequest figureRequest)
        {
            if (figureRequest.Params == null || figureRequest.Params.Count == 0)
                throw new InvalidFigureRequestException(nameof(figureRequest.Params));

            var record = figureRequest.ToFigureRecord();

            var figure = DeserializeToFigure(record);
            if (!figure.IsValid())
                throw new InvalidFigureException(figureRequest.Type, record.Params);

            var id = (await _repository.SaveAsync(record)).Id;
            return new CreatedFigureResponce()
            {
                Type = figureRequest.Type,
                Id = id
            };
        }

        public async Task<FigureAreaResponce> GetFigureAreaAsync(int id)
        {
            var record = await _repository.GetAsync(id);
            if(record==null)
                throw new NotFoundFigureException(id);

            var figure = DeserializeToFigure(record);
            if (!figure.IsValid())
                throw new CorruptedFigureException(id);

            return new FigureAreaResponce()
            {
                Id = id,
                Type = record.Type.ToString(),
                Area = figure.GetSqare()
            };
        }

        private IFigure DeserializeToFigure(FigureRecord record) 
        {
            return record.Type switch
            {
                FigureType.Circle => JsonConvert.DeserializeObject<Circle>(record.Params),
                FigureType.Triangle => JsonConvert.DeserializeObject<Triangle>(record.Params),
                _ => throw new InvalidFigureTypeException(record.Type.ToString()),
            };
        }
    }
}
