using Newtonsoft.Json;
using Robson_Totvs_Test.Enumerations;

namespace Robson_Totvs_Test.Application.DTO.Models.Request
{
    public class CreateObjectProfileRequestDTO
    {
        [JsonProperty("type")]
        public ProfileType Type { get; set; }
    }
}
