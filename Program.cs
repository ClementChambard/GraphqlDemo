using Api.Data;
using Api.Models.DAO;
using Api.Resolvers;
using Api.Resolvers.Queries;
using Api.Resolvers.Mutations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApiDbContext>();
builder.Services.AddControllers();

builder.Services.Configure<Api.Auth.TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var tokenSettings = builder.Configuration
                    .GetSection("TokenSettings").Get<Api.Auth.TokenSettings>();
                    options.TokenValidationParameters =
                        new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidIssuer = tokenSettings.Issuer,
                            ValidateIssuer = true,
                            ValidAudience = tokenSettings.Audience,
                            ValidateAudience = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key)),
                            ValidateIssuerSigningKey = true
                        };
                });

builder.Services.AddGraphQLServer()
                .AddAuthorization()
                .AddFiltering()

                .ModifyRequestOptions(o =>
                {
                    o.Complexity.Enable = true;
                    o.Complexity.MaximumAllowed = 1500;
                    o.Complexity.ApplyDefaults = true;
                    o.Complexity.DefaultComplexity = 1;
                    o.Complexity.DefaultResolverComplexity = 5;
                    o.Complexity.Calculation = context =>
                    {
                        int childComplexity = context.ChildComplexity;
                        if (context.Field.ToString().Contains("[")) childComplexity *= 5;
                        return Math.Min(context.Complexity + childComplexity, 999999);
                    };
                })
                .UsePersistedQueryPipeline()
                .AddReadOnlyFileSystemQueryStorage("./persisted_queries")
                //.AllowIntrospection(false)

                .AddQueryType<Query>()
                .AddTypeExtension<ActorQuery>()
                .AddTypeExtension<RoleQuery>()
                .AddTypeExtension<MovieQuery>()
                .AddTypeExtension<ProducerQuery>()

                .AddMutationType<Mutation>()
                .AddTypeExtension<ActorMutation>()
                .AddTypeExtension<RoleMutation>()
                .AddTypeExtension<MovieMutation>()
                .AddTypeExtension<ProducerMutation>()
                .AddTypeExtension<Api.Auth.AuthMutation>()

                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions();

builder.Services.AddScoped<ActorRepository, ActorRepository>();
builder.Services.AddScoped<MovieRepository, MovieRepository>();
builder.Services.AddScoped<RoleRepository, RoleRepository>();
builder.Services.AddScoped<ProducerRepository, ProducerRepository>();
builder.Services.AddScoped<Api.Auth.IAuthLogic, Api.Auth.AuthLogic>();

var app = builder.Build();

Database.Init(app);

app.UseWebSockets();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => {
    endpoints.MapGraphQL();
   // endpoints.MapGraphQLHttp("/graphql");
   // endpoints.MapBananaCakePop("/graphql/IDE");
   // endpoints.MapGraphQLSchema("/graphql/schema").RequireAuthorization();
   // endpoints.MapGraphQLWebSocket("/graphql/ws");
    endpoints.MapControllers();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.MapControllers();

app.Run();
