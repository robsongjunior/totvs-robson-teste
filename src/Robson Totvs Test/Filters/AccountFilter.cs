using Robson_Totvs_Test.Application.DTO.Models.Request;
using Robson_Totvs_Test.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Robson_Totvs_Test.Filters
{
    public static class AccountFilter
    {
        public static string GetSqlCommandByRequest(GetAccountFilterDTO request)
        {
            var query = "Select u.* from public.\"AspNetUsers\" as u";

            if (request != null)
            {
                if (request.Type.HasValue)
                {
                    query = string.Join(" ", query, "left join tb_profiles p on p.accountid=u.id");
                }

                query = string.Join(" ", query, "where");

                if (request.Name != null)
                {
                    query = string.Join(" ", query, $"LOWER(u.name) like LOWER('%{request.Name}%')", "and");
                }

                if (request.Email != null)
                {
                    query = string.Join(" ", query, $"LOWER(u.email) like LOWER('%{request.Email}%')", "and");
                }

                if (request.Id != null)
                {
                    query = string.Join(" ", query, $"LOWER(u.id) = LOWER('{request.Name}')", "and");
                }

                if (request.Type.HasValue)
                {
                    query = string.Join(" ", query, $"p.type={((int)request.Type)}", "and");
                }
            }
            if (query.EndsWith("and"))
            {
                query = string.Join(" ", query.Split(" ").SkipLast(1));
            }

            return query;
        }
    }
}
