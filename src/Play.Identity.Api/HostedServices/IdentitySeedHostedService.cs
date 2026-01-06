using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Options;

using Play.Identity.Api.Entities;

using Play.Identity.Api.Settings;

namespace Play.Identity.Api.HostedServices
{
    public class IdentitySeedHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IdentitySettings _identitySettings;

        public IdentitySeedHostedService(IServiceScopeFactory serviceScopeFactory, IOptions<IdentitySettings> identityOptions)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _identitySettings = identityOptions.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await CreateRoleIfNotExistAsync(Roles.Admin, roleManager);
            await CreateRoleIfNotExistAsync(Roles.Player, roleManager);

            var adminUser = await CreateAdminUserIfNotExistAsync(userManager);

            await AddUserInRoleIfNotInRole(userManager, adminUser, Roles.Admin);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private static async Task CreateRoleIfNotExistAsync(string role, RoleManager<ApplicationRole> roleManager)
        {
            var roleExists = await roleManager.RoleExistsAsync(role);

            if (!roleExists)
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = role });
            }
        }

        private async Task<ApplicationUser> CreateAdminUserIfNotExistAsync(UserManager<ApplicationUser> userManager)
        {
            var adminUser = await userManager.FindByEmailAsync(_identitySettings.AdminUserEmail);
            if (adminUser is null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = _identitySettings.AdminUserEmail,
                    Email = _identitySettings.AdminUserEmail
                };

                await userManager.CreateAsync(adminUser, _identitySettings.AdminUserPassword);
            }

            return adminUser;
        }

        private static async Task AddUserInRoleIfNotInRole(UserManager<ApplicationUser> userManager, ApplicationUser adminUser, string role)
        {
            var isInRole = await userManager.IsInRoleAsync(adminUser, role);
            if (!isInRole)
            {
                await userManager.AddToRoleAsync(adminUser, role);
            }
        }
    }
}