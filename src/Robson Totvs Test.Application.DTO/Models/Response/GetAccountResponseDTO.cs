using Robson_Totvs_Test.Application.DTO.Models.Common;
using System;
using System.Collections.Generic;

namespace Robson_Totvs_Test.Application.DTO.Models.Response
{
    public class GetAccountResponseDTO
    {
        public GetAccountResponseDTO()
        {

        }

        public GetAccountResponseDTO(string token, string passwordHash, string name, DateTime created, DateTime? modified, DateTime? lastLogin, List<ProfileObjectDTO> profiles)
        {
            Token = token;
            PasswordHash = passwordHash;
            Name = name;
            Created = created;
            Modified = modified;
            LastLogin = lastLogin;
            Profiles = profiles;
        }

        public string Token { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? Modified { get; private set; }
        public DateTime? LastLogin { get; private set; }
        public List<ProfileObjectDTO> Profiles { get; private set; }
    }
}
