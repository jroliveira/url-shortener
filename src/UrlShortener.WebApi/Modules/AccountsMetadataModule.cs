using Nancy.Metadata.Modules;
using Nancy.Swagger;
using Model = UrlShortener.WebApi.Models.Account;

namespace UrlShortener.WebApi.Modules
{
    public class AccountsMetadataModule : MetadataModule<SwaggerRouteData>
    {
        public AccountsMetadataModule()
        {
            Describe["GetAccounts"] = desc => desc.AsSwagger(with =>
            {
                with.ResourcePath("/accounts");
                with.Summary("Buscar todas as contas");
                with.Response(404, "Nenhuma conta cadastrada");
                with.Notes("Retorna as contas cadastrada");
                with.Model<Model.Get.Account>();
            });

            Describe["GetAccountById"] = desc => desc.AsSwagger(with =>
            {
                with.ResourcePath("/accounts");
                with.Summary("Buscar uma conta por id");
                with.PathParam("id", "Id da conta", true, 1);
                with.Response(404, "Conta não cadastrada");
                with.Notes("Retorna uma conta cadastrada");
                with.Model<Model.Get.Account>();
            });

            Describe["PostAccount"] = desc => desc.AsSwagger(with =>
            {
                with.ResourcePath("/accounts");
                with.Summary("Criar uma conta");
                with.Response(201, "Conta criada");
                with.Response(422, "Dados inválidos");
                with.Model<Model.Post.Account>();
                with.BodyParam<Model.Post.Account>("Objeto de conta", true);
                with.Notes("Cria uma conta com o esquema indicado");
            });

            Describe["PutAccount"] = desc => desc.AsSwagger(with =>
            {
                with.ResourcePath("/accounts");
                with.Summary("Alterar uma conta");
                with.PathParam("id", "Id da conta", true, 1);
                with.Response(204, "Conta alterada");
                with.Response(404, "Conta não cadastrada");
                with.Response(422, "Dados inválidos");
                with.Model<Model.Put.Account>();
                with.BodyParam<Model.Put.Account>("Objeto de conta", true);
                with.Notes("Altera uma conta com o esquema indicado");
            });

            Describe["DeleteAccount"] = desc => desc.AsSwagger(with =>
            {
                with.ResourcePath("/accounts");
                with.Summary("Deletar uma conta");
                with.PathParam("id", "Id da conta", true, 1);
                with.Response(204, "Conta deletada");
                with.Response(404, "Conta não cadastrada");
                with.Notes("Deleta uma conta cadastrada");
            });

            Describe["RecoverAccount"] = desc => desc.AsSwagger(with =>
            {
                with.ResourcePath("/accounts");
                with.Summary("Recuperar uma conta deletada");
                with.PathParam("id", "Id da conta", true, 1);
                with.Response(201, "Conta recuperada");
                with.Response(404, "Conta não cadastrada");
                with.Notes("Recupera uma conta deletada");
            });
        }
    }
}