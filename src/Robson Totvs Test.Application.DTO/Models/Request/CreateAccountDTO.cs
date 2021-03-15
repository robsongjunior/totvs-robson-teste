using DataAnnotationsExtensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Robson_Totvs_Test.Application.DTO.Models.Request
{
    public class CreateAccountDTO : IValidatableObject
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Email]
        [Required]
        [StringLength(256)]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [StringLength(12)]
        [DataType(DataType.Password)]
        [JsonProperty("password")]
        public string Password { get; set; }

        [Required]
        [JsonProperty("profiles")]
        public CreateObjectProfileRequestDTO[] Profiles { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();
            var grouppedProfiles = this.Profiles
                .GroupBy(myProfile => myProfile.Type, myProfile => myProfile);
            var hasMoreThanOne = grouppedProfiles.Any(x => x.Count() > 1);

            if (hasMoreThanOne)
            {
                result.Add(new ValidationResult(
                    errorMessage: "The profile must be unique.",
                    memberNames: new string[1] { nameof(this.Profiles).ToLower() }));
            }

            return result;
        }
    }
}
