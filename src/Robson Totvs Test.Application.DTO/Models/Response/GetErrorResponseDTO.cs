using System.Net;

namespace Robson_Totvs_Test.Application.DTO.Models.Response
{
    public class GetErrorResponseDTO
    {
        public GetErrorResponseDTO()
        {

        }

        public GetErrorResponseDTO(HttpStatusCode statusCode, params string[] errors)
        {
            StatusCode = (int)statusCode;
            Errors = errors;
        }

        public int StatusCode { get; set; }
        public string[] Errors { get; set; }
    }
}
