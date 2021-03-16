using Newtonsoft.Json;
using Robson_Totvs_Test.Enumerations;

namespace Robson_Totvs_Test.Application.DTO.Models.Common
{
    public class ProfileObjectDTO
    {
        public ProfileObjectDTO()
        {

        }

        public ProfileObjectDTO(ProfileType type)
        {
            Type = type;
        }

        [JsonProperty("type")]
        public ProfileType Type { get; set; }
    }
}
