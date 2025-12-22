using Play.Identity.Api.Entities;

namespace Play.Identity.Api.Mappers
{
    public static class UserMappers
    {
        public static UserDto AsDto(this ApplicationUser applicationUser)
        {
            return new UserDto(applicationUser.Id,
                               applicationUser.UserName,
                               applicationUser.Email,
                               applicationUser.Gil,
                               applicationUser.CreatedDate);
        }
    }
}