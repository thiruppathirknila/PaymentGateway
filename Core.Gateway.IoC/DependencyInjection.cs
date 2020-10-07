using Core.Gateway.Data.Queries;
using Microsoft.Extensions.DependencyInjection;
using Payment.Data.Repository;
using Core.Gateway.Interfaces;
using Core.Gateway.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Gateway.Domain.Interfaces;
using Core.Gateway.Domain.Services;

namespace Core.Gateway.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<ICommandText, CommandText>();
            services.AddTransient<IGenericRepository, GenericRepository>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IMessageService, MessageService>();
            return services;
        }
    }
}
