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
        public async Task<int> SaveFigureAsync(FigureRequest figure)
        {
            if (!Enum.TryParse<FigureType>(figure.Type, out var type))
                throw new Exception($"The figure type '{figure.Type}' is invalid.");

            if (figure.Params == null || figure.Params.Count == 0)
                throw new Exception($"The property '{nameof(figure.Params)}' should not be empty.");

            string jsonParams = JsonConvert.SerializeObject(figure.Params);
            var record = new FigureRecord()
            {
                Type = type,
                Params = jsonParams
            };
            var id = (await _repository.SaveAsync(record)).Id;
            return id;
        }

        public async Task<int> GetFigureSquareAsync(int id)
        {
            var record = await _repository.GetAsync(id);
            return record.Id;
        }
    }
}
