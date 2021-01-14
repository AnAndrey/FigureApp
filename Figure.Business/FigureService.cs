using Figure.Contracts;
using Figure.Contracts.Db;
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
                throw new Exception($"The figure type '{figureRequest.Type}' is invalid.");

            if (figureRequest.Params == null || figureRequest.Params.Count == 0)
                throw new Exception($"The property '{nameof(figureRequest.Params)}' should not be empty.");

            string jsonParams = JsonConvert.SerializeObject(figureRequest.Params);
            var record = new FigureRecord()
            {
                Type = type,
                Params = jsonParams
            };

            var figure = DeserializeToFigure(record);
            if (!figure.IsValid())
                throw new Exception($"The figure '{type}' with params '{jsonParams}' is invalid.");

            var id = (await _repository.SaveAsync(record)).Id;
            return id;
        }

        public async Task<double> GetFigureAreaAsync(int id)
        {
            var record = await _repository.GetAsync(id);
            var figure = DeserializeToFigure(record);
            if (!figure.IsValid())
                throw new Exception($"The figure with '{id}' is invalid.");
            return figure.GetSqare();
        }

        private IFigure DeserializeToFigure(FigureRecord record) 
        {
            switch (record.Type) 
            {
                case FigureType.Circle:
                    return JsonConvert.DeserializeObject<Circle>(record.Params);
                default:
                    throw new Exception($"Invalid figure type '{record.Type}'");
            }
        }
    }
}
