using Robson_Totvs_Test.Enumerations;

namespace Robson_Totvs_Test.Application.DTO.Models.Request
{
    public class GetAccountFilterDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ProfileType? Type { get; set; }
    }
}
