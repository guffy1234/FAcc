using FuelAcc.Application.Interface.Accounting;
using FuelAcc.Application.Interface.Exceptions;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities;
using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Entities.Documents;
using FuelAcc.Persistence.Contexts;
using FuelAcc.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FuelAcc.Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInjectionPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            //});
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.RegisterEntityRepositories<Partner>();
            services.RegisterEntityRepositories<Product>();
            services.RegisterEntityRepositories<Branch>();
            services.RegisterEntityRepositories<Storage>();
            services.RegisterEntityRepositories<OrderIn>();
            services.RegisterEntityRepositories<OrderMove>();
            services.RegisterEntityRepositories<OrderOut>();

            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<ITransactionsRepository, TransactionsRepository>();
            services.AddScoped<IReportsRepository, ReportsRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static void RegisterEntityRepositories<ENTITY>(this IServiceCollection services)
            where ENTITY : class, IRootEntity, ISoftDeleted
        {
            services.AddScoped<IEntityWriteRepository<ENTITY>, EntityWriteRepository<ENTITY>>();
            services.AddScoped<IEntityReadRepository<ENTITY>, EntityReadRepository<ENTITY>>();
        }
    }
}