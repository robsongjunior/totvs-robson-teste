using Robson_Totvs_Test.Enumerations;
using System;

namespace Robson_Totvs_Test.Domain.Entities
{
    public class ProfileObject
    {
        public ProfileObject(ProfileType type)
        {
            this.Type = type;
        }

        public Guid Id { get; set; }
        public string AccountId { get; set; }
        public ProfileType Type { get; set; }
    }
}
