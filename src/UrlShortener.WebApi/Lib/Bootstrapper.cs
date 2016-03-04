using AutoMapper;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Hal.Configuration;
using Nancy.Json;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using Simple.Data;
using UrlShortener.Infrastructure;
using UrlShortener.Infrastructure.Data.Filter;
using UrlShortener.Infrastructure.Data.Filter.Simple.Data;
using UrlShortener.WebApi.Lib.Hal;
using UrlShortener.WebApi.Lib.Mappings;
using UrlShortener.WebApi.Lib.Validators;

namespace UrlShortener.WebApi.Lib
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            JsonSettings.MaxJsonLength = int.MaxValue;

            base.ApplicationStartup(container, pipelines);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            TokenAuthentication.Enable(pipelines, new TokenAuthenticationConfiguration(container.Resolve<ITokenizer>()));

            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx => ctx.Response
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE,OPTIONS")
                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type, X-Requested-With, api_key, Authorization")
                .WithHeader("Access-Control-Allow-Credentials", "true"));

            pipelines.OnError.AddItemToEndOfPipeline(HandlerError.Config);

            base.RequestStartup(container, pipelines, context);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.AddDirectory("public", "Content");
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer existingContainer)
        {
            existingContainer.Register<IProvideHalTypeConfiguration>(Hypermedia.Configuration());
            existingContainer.Register<JsonSerializer, CustomJsonSerializer>();
            existingContainer.Register<ITokenizer>(new Tokenizer());
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer existingContainer, NancyContext context)
        {
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccountProfile>();
                cfg.AddProfile<UrlProfile>();
                cfg.AddProfile<PagedProfile>();
            });

            existingContainer.Register((container, overloads) => autoMapperConfig.CreateMapper());
            existingContainer.Register<PartialUpdater>();

            /* Filters */
            existingContainer.Register<ISkip<Filter>, Skip>();
            existingContainer.Register<ILimit<Filter>, Limit>();
            existingContainer.Register<IOrder<Filter, ObjectReference>, Order>();
            existingContainer.Register<IOrderDirection<Filter, OrderByDirection>, OrderDirection>();
            existingContainer.Register<IWhere<Filter, SimpleExpression>, Where>();

            /* Commands */
            existingContainer.Register<Infrastructure.Data.Commands.Account.CreateCommand>();
            existingContainer.Register<Infrastructure.Data.Commands.Account.UpdateCommand>();
            existingContainer.Register<Infrastructure.Data.Commands.Account.ExcludeCommand>();
            existingContainer.Register<Infrastructure.Data.Commands.Url.CreateCommand>();
            existingContainer.Register<Infrastructure.Data.Commands.Url.ExcludeCommand>();

            /* Queries */
            existingContainer.Register<Infrastructure.Data.Queries.Account.GetAll>();
            existingContainer.Register<Infrastructure.Data.Queries.Account.GetById>();
            existingContainer.Register<Infrastructure.Data.Queries.Account.GetByEmail>();
            existingContainer.Register<Infrastructure.Data.Queries.Url.GetAll>();
            existingContainer.Register<Infrastructure.Data.Queries.Url.GetByUrl>();

            /* Validators */
            existingContainer.Register<AccountValidator>();
            existingContainer.Register<UrlValidator>();
        }
    }
}