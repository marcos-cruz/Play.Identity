using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

using Play.Common.Settings;
using Play.Identity.Api.Entities;
using Play.Identity.Api.Settings;

var builder = WebApplication.CreateBuilder(args);
const string AllowedOriginSetting = "AllowedOrigin";

// Add services to the container.

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

AddMongoDbSerializers();

var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
if (serviceSettings is null)
{
    throw new InvalidOperationException($"No '{nameof(ServiceSettings)}' section found in configuration.");
}

var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
if (mongoDbSettings is null)
{
    throw new InvalidOperationException($"No '{nameof(MongoDbSettings)}' section found in configuration.");
}

var identityServerSettings = new IdentityServerSettings();

builder.Services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
                (
                    mongoDbSettings.ConnectionString,
                    serviceSettings.ServiceName
                );

builder.Services.AddIdentityServer()
                .AddInMemoryApiScopes(identityServerSettings.ApiScopes)
                .AddInMemoryClients(identityServerSettings.Clients);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var allowedOriginSettings = builder.Configuration.GetSection(AllowedOriginSetting)
                                                     .Get<string[]>();

    if (allowedOriginSettings != null && allowedOriginSettings.Length > 0)
    {
        app.UseCors(builder =>
        {
            builder.WithOrigins(allowedOriginSettings)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    }
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();

static void AddMongoDbSerializers()
{
    BsonSerializer.TryRegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
    BsonSerializer.TryRegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
    BsonSerializer.TryRegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));
    BsonSerializer.TryRegisterSerializer(new DecimalSerializer(MongoDB.Bson.BsonType.String));
}