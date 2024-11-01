﻿using FluentValidation;
using FuelAcc.Application.Interface.Accounting;
using FuelAcc.Application.UseCases.Accounting;
using FuelAcc.Application.UseCases.Authorization;
using FuelAcc.Application.UseCases.Commons.Behaviours;
using FuelAcc.Application.UseCases.Commons.Commands;
using FuelAcc.Application.UseCases.Commons.Commands.Handlers;
using FuelAcc.Application.UseCases.Commons.Events;
using FuelAcc.Application.UseCases.Commons.Events.Handlers;
using FuelAcc.Application.UseCases.Commons.Queries;
using FuelAcc.Application.UseCases.Commons.Queries.Handlers;
using FuelAcc.Application.UseCases.Dictionaries.Branches;
using FuelAcc.Application.UseCases.Dictionaries.Partners;
using FuelAcc.Application.UseCases.Dictionaries.Storages;
using FuelAcc.Application.UseCases.Documents.OrdersIn;
using FuelAcc.Application.UseCases.Documents.OrdersMove;
using FuelAcc.Application.UseCases.Documents.OrdersOut;
using FuelAcc.Application.UseCases.Products;
using FuelAcc.Application.UseCases.Reports.Rets;
using FuelAcc.Application.UseCases.Reports.Transactions;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Entities.Documents;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FuelAcc.Application.UseCases
{
    public static class ConfigureServices
    {
        public static void AddInjectionApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            services.AddDictionary<Branch, BranchDto>();
            services.AddDictionary<Partner, PartnerDto>();
            services.AddDictionary<Storage, StorageDto>();
            services.AddDictionary<Product, ProductDto>();

            services.AddDocument<OrderIn, OrderInDto>();
            services.AddDocument<OrderOut, OrderOutDto>();
            services.AddDocument<OrderMove, OrderMoveDto>();

            services.AddScoped<ITransactionsService, TransactionsService>();

            services.AddTransient<IDocumentTransactionsProcessor<OrderMove>, OrderMoveTransactionsProcessor>();
            services.AddTransient<IDocumentTransactionsProcessor<OrderIn>, OrderInTransactionsProcessor>();
            services.AddTransient<IDocumentTransactionsProcessor<OrderOut>, OrderOutTransactionsProcessor>();

            // reports
            services.AddTransient(typeof(IRequestHandler<ReportTransactionsQuery, IAsyncEnumerable<ReportTransactionView>>),
               typeof(ReportTransactionsHandler));
            services.AddTransient(typeof(IRequestHandler<ReportRestsQuery, IAsyncEnumerable<ReportRestView>>),
              typeof(ReportRestsHandler));
        }

        public static void AddDictionary<ENTITY, DTO>(this IServiceCollection services)
            where DTO : class
            where ENTITY : class, IDictionaryEntity, new()
        {
            // Queries
            services.AddTransient(typeof(IRequestHandler<GetAllQuery<DTO>, IAsyncEnumerable<DTO>>), 
                typeof(GetAllHandler<ENTITY, DTO, DictionaryAuthorizationPoint<ENTITY>>));
            services.AddTransient(typeof(IRequestHandler<GetByIdQuery<DTO>, DTO>), 
                typeof(GetByIdHandler<ENTITY, DTO, DictionaryAuthorizationPoint<ENTITY>>));
            // Commands
            services.AddTransient(typeof(IRequestHandler<CreateCommand<DTO>, Unit>), 
                typeof(CreateCommandHandler<ENTITY, DTO, DictionaryAuthorizationPoint<ENTITY>>));
            services.AddTransient(typeof(IRequestHandler<DeleteCommand<DTO>, Unit>), 
                typeof(DeleteCommandHandler<ENTITY, DTO, DictionaryAuthorizationPoint<ENTITY>>));
            services.AddTransient(typeof(IRequestHandler<UpdateCommand<DTO>, Unit>), 
                typeof(UpdateCommandHandler<ENTITY, DTO, DictionaryAuthorizationPoint<ENTITY>>));
            // Events
            services.AddTransient(typeof(IRequestHandler<CreateEvent<ENTITY>, Unit>), typeof(CreateEventHandler<ENTITY>));
            services.AddTransient(typeof(IRequestHandler<DeleteEvent<ENTITY>, Unit>), typeof(DeleteEventHandler<ENTITY>));
            services.AddTransient(typeof(IRequestHandler<UpdateEvent<ENTITY>, Unit>), typeof(UpdateEventHandler<ENTITY>));
        }

        public static void AddDocument<ENTITY, DTO>(this IServiceCollection services)
            where DTO : class
            where ENTITY : class, IDocumentEntity, new()
        {
            // Queries
            services.AddTransient(typeof(IRequestHandler<GetAllQuery<DTO>, IAsyncEnumerable<DTO>>), 
                typeof(GetAllHandler<ENTITY, DTO, DocumentAuthorizationPoint<ENTITY>>));
            services.AddTransient(typeof(IRequestHandler<GetByIdQuery<DTO>, DTO>), 
                typeof(GetByIdHandler<ENTITY, DTO, DocumentAuthorizationPoint<ENTITY>>));
            // Commands
            services.AddTransient(typeof(IRequestHandler<CreateCommand<DTO>, Unit>), 
                typeof(CreateCommandHandler<ENTITY, DTO, DocumentAuthorizationPoint<ENTITY>>));
            services.AddTransient(typeof(IRequestHandler<DeleteCommand<DTO>, Unit>), 
                typeof(DeleteCommandHandler<ENTITY, DTO, DocumentAuthorizationPoint<ENTITY>>));
            services.AddTransient(typeof(IRequestHandler<UpdateCommand<DTO>, Unit>), 
                typeof(UpdateCommandHandler<ENTITY, DTO, DocumentAuthorizationPoint<ENTITY>>));
            // Events
            services.AddTransient(typeof(IRequestHandler<CreateEvent<ENTITY>, Unit>), typeof(CreateDocumentEventHandler<ENTITY>));
            services.AddTransient(typeof(IRequestHandler<DeleteEvent<ENTITY>, Unit>), typeof(DeleteDocumentEventHandler<ENTITY>));
            services.AddTransient(typeof(IRequestHandler<UpdateEvent<ENTITY>, Unit>), typeof(UpdateDocumentEventHandler<ENTITY>));
        }
    }
}