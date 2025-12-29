using Duende.IdentityServer.Models;

namespace Play.Identity.Api.Settings
{
    public class IdentityServerSettings
    {
        // ApiScopes representa os diferentes nívels de acesso que os clientes podem solicitar.
        // Cada ApiScope define um escopo específico de acesso a uma API protegida.
        public IReadOnlyCollection<ApiScope> ApiScopes { get; set; } = Array.Empty<ApiScope>();

        // Clientes que tem autorização pra acessar o microserviço.
        public IReadOnlyCollection<Client> Clients { get; set; } = Array.Empty<Client>();
    }
}