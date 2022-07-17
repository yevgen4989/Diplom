﻿namespace WebFramework.FirebaseInfrastructure.Middleware
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    using System.Linq;

    using FirebaseInfrastructure.Utils;
    using FirebaseAdmin.Auth;

    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IFirebaseAdminUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            FirebaseToken user = await jwtUtils.ValidateToken(token);

            if (user != null)
                context.Items["user"] = user;

            await _next(context);
        }
    }
}
