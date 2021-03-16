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
            var query = "Select * from public.\"AspNetUsers\"";

            if (request != null)
            {
                if (request?.IsEmpty() == false)
                {
                    query = string.Join(" ", query, "where");

                    if (request.Name != null)
                    {
                        query = string.Join(" ", query, $"LOWER(name) like LOWER('%{request.Name}%')", "and");
                    }

                    if (request.Email != null)
                    {
                        query = string.Join(" ", query, $"LOWER(email) like LOWER('%{request.Email}%')", "and");
                    }

                    if (request.Id != null)
                    {
                        query = string.Join(" ", query, $"LOWER(id) = LOWER('{request.Name}')", "and");
                    }
                }
                if (query.EndsWith("and"))
                {
                    query = string.Join(" ", query.Split(" ").SkipLast(1));
                }
            }

            return query;
        }
    }
}
