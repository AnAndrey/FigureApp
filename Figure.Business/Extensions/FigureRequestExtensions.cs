using Figure.Contracts;
using Figure.Contracts.Db;
using Figure.Contracts.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Figure.Business
{
    public static class FigureRequestExtensions
    {
        public static FigureRecord ToFigureRecord(this FigureRequest request)
        {
            if (!Enum.TryParse<FigureType>(request.Type, out var type))
                throw new FigureException($"The figure type '{request.Type}' is invalid.");

            string jsonParams = JsonConvert.SerializeObject(request.Params);
            return new FigureRecord()
            {
                Type = type,
                Params = jsonParams
            };
        }
    }
}
