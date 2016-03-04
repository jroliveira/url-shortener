using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using Nancy.Security;
using Newtonsoft.Json;
using UrlShortener.Infrastructure;
using UrlShortener.Infrastructure.Data.Commands.Account;
using UrlShortener.Infrastructure.Data.Queries.Account;
using UrlShortener.Infrastructure.Exceptions;
using UrlShortener.WebApi.Lib.Exceptions;
using UrlShortener.WebApi.Lib.Validators;

namespace UrlShortener.WebApi.Modules
{
    public class AccountsModule : BaseModule
    {
        private readonly GetAll _getAll;
        private readonly GetById _getById;
        private readonly CreateCommand _create;
        private readonly UpdateCommand _update;
        private readonly ExcludeCommand _exclude;
        private readonly AccountValidator _validator;
        private readonly IMapper _mapper;

        public AccountsModule(
            GetAll getAll,
            GetById getById,
            CreateCommand create,
            UpdateCommand update,
            ExcludeCommand exclude,
            AccountValidator validator, 
            IMapper mapper)
            : base("accounts")
        {
            _getAll = getAll;
            _getById = getById;
            _create = create;
            _update = update;
            _exclude = exclude;
            _validator = validator;
            _mapper = mapper;

            this.RequiresAuthentication();

            Get["/", true] = All;
            Get["/{id}", true] = ById;
            Post["/", true] = Create;
            Put["/{id}", true] = Update;
            Delete["/{id}", true] = Exclude;
        }

        private async Task<dynamic> All(dynamic _, CancellationToken ct)
        {
            var entities = await _getAll.GetResult(QueryStringFilter);

            if (entities == null)
            {
                throw new NotFoundException("Resource 'accounts' with filter passed could not be found");
            }

            var models = _mapper.Map<Paged<Models.Account.Get.Account>>(entities);

            return Negotiate.WithModel(models);
        }

        private async Task<dynamic> ById(dynamic parameters, CancellationToken ct)
        {
            int id = parameters.id;
            var entity = await _getById.GetResult(id);

            if (entity == null)
            {
                throw new NotFoundException("Resource 'accounts' with id {0} could not be found", id);
            }

            var model = _mapper.Map<Models.Account.Get.Account>(entity);

            return Negotiate.WithModel(model);
        }

        private async Task<dynamic> Create(dynamic _, CancellationToken ct)
        {
            var model = this.Bind<Models.Account.Post.Account>();
            var validateResult = _validator.Validate(model);

            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

            var entity = _mapper.Map<Entities.Account>(model);

            var insertedId = await _create.Execute(entity);

            var response = new
            {
                Id = insertedId
            };

            return
                Negotiate
                    .WithModel(response)
                    .WithStatusCode(HttpStatusCode.Created);
        }

        private async Task<dynamic> Update(dynamic parameters, CancellationToken ct)
        {
            int id = parameters.id;
            dynamic changedModel = JsonConvert.DeserializeObject(Request.Body.AsString());

            await _update.Execute(id, changedModel);

            return Negotiate.WithStatusCode(HttpStatusCode.NoContent);
        }

        private async Task<dynamic> Exclude(dynamic parameters, CancellationToken ct)
        {
            int id = parameters.id;

            await _exclude.Execute(id);

            return Negotiate.WithStatusCode(HttpStatusCode.NoContent);
        }
    }
}