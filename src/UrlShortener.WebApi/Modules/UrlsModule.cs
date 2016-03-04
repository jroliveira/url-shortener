using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using UrlShortener.Infrastructure;
using UrlShortener.Infrastructure.Data.Commands.Url;
using UrlShortener.Infrastructure.Data.Queries.Url;
using UrlShortener.Infrastructure.Exceptions;
using UrlShortener.WebApi.Lib.Exceptions;
using UrlShortener.WebApi.Lib.Validators;

namespace UrlShortener.WebApi.Modules
{
    public class UrlsModule : BaseModule
    {
        private readonly GetAll _getAll;
        private readonly GetByUrl _getByShortened;
        private readonly CreateCommand _create;
        private readonly ExcludeCommand _exclude;
        private readonly UrlValidator _validator;
        private readonly IMapper _mapper;

        public UrlsModule(
            GetAll getAll,
            GetByUrl getByShortened,
            CreateCommand create,
            ExcludeCommand exclude,
            UrlValidator validator,
            IMapper mapper)
        {
            _getAll = getAll;
            _getByShortened = getByShortened;
            _create = create;
            _exclude = exclude;
            _validator = validator;
            _mapper = mapper;

            this.RequiresAuthentication();

            Get["urls/", true] = All;
            Get["accounts/{id}/urls/", true] = All;
            Get["urls/{url}", true] = ByUrl;
            Post["urls/", true] = Create;
            Delete["urls/{id}", true] = Exclude;
        }

        private async Task<dynamic> All(dynamic parameters, CancellationToken ct)
        {
            int? accountId = parameters.id;
            var entities = await _getAll.GetResult(QueryStringFilter, accountId);

            if (entities == null)
            {
                throw new NotFoundException("Resource 'urls' with filter passed could not be found");
            }

            var models = _mapper.Map<Paged<Models.Url.Get.Url>>(entities);

            return Negotiate.WithModel(models);
        }

        private async Task<dynamic> ByUrl(dynamic parameters, CancellationToken ct)
        {
            string url = parameters.url;
            var entity = await _getByShortened.GetResult(url);

            if (entity == null)
            {
                throw new NotFoundException("Resource 'urls' with url {0} could not be found", url);
            }

            var model = _mapper.Map<Models.Url.Get.Url>(entity);

            return Negotiate.WithModel(model);
        }

        private async Task<dynamic> Create(dynamic _, CancellationToken ct)
        {
            var model = this.Bind<Models.Url.Post.Url>();
            var validateResult = _validator.Validate(model);

            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

            var entity = _mapper.Map<Entities.Url>(model);

            entity = await _create.Execute(entity);

            var response = new
            {
                entity.Id,
                Address = string.Format("{0}/{1}", Request.Url, entity.Shortened)
            };

            return
                Negotiate
                    .WithModel(response)
                    .WithStatusCode(HttpStatusCode.Created);
        }

        private async Task<dynamic> Exclude(dynamic parameters, CancellationToken ct)
        {
            int id = parameters.id;

            await _exclude.Execute(id);

            return Negotiate.WithStatusCode(HttpStatusCode.NoContent);
        }
    }
}
