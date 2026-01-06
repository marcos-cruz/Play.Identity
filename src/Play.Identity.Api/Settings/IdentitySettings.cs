namespace Play.Identity.Api.Settings
{
    public class IdentitySettings
    {
        public string AdminUserEmail { get; init; } = default!;

        public string AdminUserPassword { get; init; } = default!;

        public decimal StartingGil { get; init; } = 0.00m;
    }
}