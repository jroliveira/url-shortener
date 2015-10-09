using AutoMapper;
using LightInject;
using LightInject.Nancy;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Json;
using Newtonsoft.Json;
using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Filter.Data;
using UrlShortener.WebApi.Infrastructure.Filter.Data.Simple.Data;
using Commands = UrlShortener.WebApi.Infrastructure.Data.Commands;
using Queries = UrlShortener.WebApi.Infrastructure.Data.Queries;

namespace UrlShortener.WebApi.Lib
{
    public class Bootstrapper : LightInjectNancyBootstrapper
    {
        protected override void ApplicationStartup(IServiceContainer container, IPipelines pipelines)
        {
            JsonSettings.MaxJsonLength = int.MaxValue;

            AutoMapperConfig.RegisterProfiles();

            base.ApplicationStartup(container, pipelines);
        }

        protected override void RequestStartup(IServiceContainer container, IPipelines pipelines, NancyContext context)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx => ctx.Response
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE,OPTIONS")
                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type, X-CSRF-Token, X-Requested-With, api_key, Authorization"));

            base.RequestStartup(container, pipelines, context);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.AddDirectory("docs", "swagger-ui");
        }

        protected override void ConfigureApplicationContainer(IServiceContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            existingContainer.Register<JsonSerializer, CustomJsonSerializer>();
            existingContainer.Register<PartialUpdater>();
            existingContainer.Register(factory => Mapper.Engine);

            /* Filters */
            existingContainer.Register<ISkip, Skip>();
            existingContainer.Register<ILimit, Limit>();
            existingContainer.Register<IOrder<ObjectReference>, Order>();
            existingContainer.Register<IOrderDirection<OrderByDirection>, OrderDirection>();
            existingContainer.Register<IWhere<SimpleExpression>, Where>();

            /* Commands */
            existingContainer.Register<Commands.Account.CreateCommand>();
            existingContainer.Register<Commands.Account.UpdateCommand>();
            existingContainer.Register<Commands.Account.ExcludeCommand>();
            existingContainer.Register<Commands.Account.RecoverCommand>();
            existingContainer.Register<Commands.Url.CreateCommand>();
            existingContainer.Register<Commands.Url.ExcludeCommand>();
            existingContainer.Register<Commands.Url.RecoverCommand>();

            /* Queries */
            existingContainer.Register<Queries.Account.GetById>();
            existingContainer.Register<Queries.Account.GetAll>();
            existingContainer.Register<Queries.Url.GetAll>();
            existingContainer.Register<Queries.Url.GetByShortened>();

            /* Validators */
            existingContainer.Register<Validators.Account.Post.AccountValidator>();
            existingContainer.Register<Validators.Account.Put.AccountValidator>();
            existingContainer.Register<Validators.Url.UrlValidator>();
        }
    }
}