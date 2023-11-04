using AddressConsultation.Application.DTO;
using AddressConsultation.Infra.Cache.Implements.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.API.Midwares.BlackList
{
    public class BlacklistCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IBlackListCache<TokenDTO> _blackListCache;

        public BlacklistCheckMiddleware(RequestDelegate next, IBlackListCache<TokenDTO> blackListCache)
        {
            _next = next;
            _blackListCache = blackListCache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var blacklistedToken = await _blackListCache.Get(token);

                if (blacklistedToken != null)
                {
                    context.Response.StatusCode = 401;
                    return;
                }
            }

            await _next(context);
        }
    }
}
