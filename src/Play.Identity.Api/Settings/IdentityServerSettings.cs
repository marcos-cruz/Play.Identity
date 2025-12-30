using Duende.IdentityServer.Models;

namespace Play.Identity.Api.Settings
{
    public class IdentityServerSettings
    {
        // ApiScopes representa os diferentes nívels de acesso que os clientes podem solicitar.
        // Cada ApiScope define um escopo específico de acesso a uma API protegida.
        public IReadOnlyCollection<ApiScope> ApiScopes { get; init; } = default!;

        public IReadOnlyCollection<ApiResource> ApiResources { get; init; } = default!;

        // Clientes que tem autorização pra acessar o microserviço.
        public IReadOnlyCollection<Client> Clients { get; init; } = default!;

        public IReadOnlyCollection<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
}