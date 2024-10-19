
using System.Text.Json.Serialization;

namespace Ucode.Core.Responses
{
    public class Response<TData> // TData - é Genérico
    {
      
        private readonly int _code;

        [JsonConstructor]
        public Response() // Contrustror Parameterless - Sem Parametro 
        {
            _code = Configuration.DefaultStatusCode;
        }

        public Response(TData? data, int code = Configuration.DefaultStatusCode, string? message = null)
        {
            Data = data;
            _code = code;
            Message = message;            
        }

        public TData? Data { get; set; }
        public string? Message { get; set; }

        [JsonIgnore]
        public bool IsSucess => _code is >= 200 and <= 299;
    }
}
