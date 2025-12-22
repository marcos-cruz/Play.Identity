using AspNetCore.Identity.MongoDbCore.Models;

using MongoDbGenericRepository.Attributes;

using Play.Common.Entities;

namespace Play.Identity.Api.Entities
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>, IEntity
    {
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

        public bool Deleted { get; set; } = false;
    }
}