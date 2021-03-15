using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Robson_Totvs_Test.Domain.Entities
{
    public class Account : IdentityUser
    {
        public Account()
        {

        }

        public Account(string name, string email, List<ProfileObject> profiles)
        {
            this.Name = name;
            this.Profiles = profiles;
            this.Created = DateTime.Now;
            this.UserName = this.Email = email;
        }

        public string Name { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? Modified { get; private set; }
        public DateTime? LastLogin { get; private set; }
        public List<ProfileObject> Profiles { get; private set; }
    }
}
