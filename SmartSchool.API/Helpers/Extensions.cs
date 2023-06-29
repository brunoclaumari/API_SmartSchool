﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Helpers
{
    public static class Extensions
    {

        public static void AddPagination(this HttpResponse response,
            int currentPage, int itemsPerPage, int totalItems, int totalPages 
        )
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);

            //Formata o Json para que ele seja camelCase
            var camelFormater = new JsonSerializerSettings();
            camelFormater.ContractResolver = new CamelCasePropertyNamesContractResolver();

            response.Headers.Add("Pagination",JsonConvert.SerializeObject(paginationHeader, camelFormater));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }


    }
}
