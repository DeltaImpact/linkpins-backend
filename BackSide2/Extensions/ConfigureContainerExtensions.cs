﻿using BackSide2.BL.Authorize;
using BackSide2.BL.BoardPinService;
using BackSide2.BL.BoardService;
using BackSide2.BL.ParsePageService;
using BackSide2.BL.PinService;
using BackSide2.BL.ProfileService;
using BackSide2.BL.UsersConnections;
using BackSide2.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BackSide2.Extensions
{
    public static class ConfigureContainerExtensions
    {
        public static void AddRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void AddScopedServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddScoped<IParsePageService, ParsePageService>();
            serviceCollection.AddScoped<IBoardService, BoardService>();
            serviceCollection.AddScoped<IPinService, PinService>();
            serviceCollection.AddScoped<IProfileService, ProfileService>();
            serviceCollection.AddScoped<IBoardPinService, BoardPinService>();
            serviceCollection.AddScoped<IConnectionMapping, ConnectionMapping>();
        }
    }
}
