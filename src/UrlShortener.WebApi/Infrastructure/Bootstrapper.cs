using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Json;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Data.Filter;
using UrlShortener.WebApi.Infrastructure.Data.Filter.Simple.Data;
using UrlShortener.WebApi.Infrastructure.Validators;
using Commands = UrlShortener.WebApi.Infrastructure.Data.Commands;
using Queries = UrlShortener.WebApi.Infrastructure.Data.Queries;

namespace UrlShortener.WebApi.Infrastructure
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            JsonSettings.MaxJsonLength = int.MaxValue;

            AutoMapperConfig.RegisterProfiles();

            base.ApplicationStartup(container, pipelines);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx => ctx.Response
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE,OPTIONS")
                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type, X-CSRF-Token, X-Requested-With, api_key, Authorization"));

            pipelines.OnError.AddItemToEndOfPipeline(HandlerError.Config);

            base.RequestStartup(container, pipelines, context);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.AddDirectory("public", "public");
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            existingContainer.Register<JsonSerializer, CustomJsonSerializer>();
            existingContainer.Register<PartialUpdater>();

            /* Filters */
            existingContainer.Register<ISkip<Filter>, Skip>();
            existingContainer.Register<ILimit<Filter>, Limit>();
            existingContainer.Register<IOrder<Filter, ObjectReference>, Order>();
            existingContainer.Register<IOrderDirection<Filter, OrderByDirection>, OrderDirection>();
            existingContainer.Register<IWhere<Filter, SimpleExpression>, Where>();

            /* Commands */
            existingContainer.Register<Commands.Account.CreateCommand>();
            existingContainer.Register<Commands.Account.UpdateCommand>();
            existingContainer.Register<Commands.Account.ExcludeCommand>();
            existingContainer.Register<Commands.Url.CreateCommand>();
            existingContainer.Register<Commands.Url.ExcludeCommand>();

            /* Queries */
            existingContainer.Register<Queries.Account.GetById>();
            existingContainer.Register<Queries.Account.GetAll>();
            existingContainer.Register<Queries.Url.GetAll>();
            existingContainer.Register<Queries.Url.GetByUrl>();

            /* Validators */
            existingContainer.Register<AccountValidator>();
            existingContainer.Register<UrlValidator>();
        }
    }
}