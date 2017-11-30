﻿using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace HyperMedia
{
    /// <summary>
    /// Interface for any class that wants to enrich a response
    /// </summary>
    public interface IResponseEnricher
    {

        bool CanEnrich(ResultExecutingContext context);

        Task Enrich(ResultExecutingContext context);
    }
}