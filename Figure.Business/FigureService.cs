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
        public async Task<int> SaveFigureAsync(FigureRequest figureRequest)
        {
            if (!Enum.TryParse<FigureType>(figureRequest.Type, out var type))
                throw new FigureException($"The figure type '{figureRequest.Type}' is invalid.");

            if (figureRequest.Params == null || figureRequest.Params.Count == 0)
                throw new FigureException($"The property '{nameof(figureRequest.Params)}' should not be empty.");

            string jsonParams = JsonConvert.SerializeObject(figureRequest.Params);
            var record = new FigureRecord()
            {
                Type = type,
                Params = jsonParams
            };

            var figure = DeserializeToFigure(record);
            if (!figure.IsValid())
                throw new FigureException($"The figure '{type}' with params '{jsonParams}' is invalid.");

            var id = (await _repository.SaveAsync(record)).Id;
            return id;
        }

        public async Task<double> GetFigureAreaAsync(int id)
        {
            var record = await _repository.GetAsync(id);
            var figure = DeserializeToFigure(record);
            if (!figure.IsValid())
                throw new FigureException($"The figure with '{id}' is invalid.");
            return figure.GetSqare();
        }

        private IFigure DeserializeToFigure(FigureRecord record) 
        {
            return record.Type switch
            {
                FigureType.Circle => JsonConvert.DeserializeObject<Circle>(record.Params),
                FigureType.Triangle => JsonConvert.DeserializeObject<Triangle>(record.Params),
                _ => throw new FigureException($"Invalid figure type '{record.Type}'"),
            };
        }
    }
}
