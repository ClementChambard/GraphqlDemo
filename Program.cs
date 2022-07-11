using Api.Data;
using Api.Models.DAO;
using Api.Resolvers;
using Api.Resolvers.Queries;
using Api.Resolvers.Mutations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApiDbContext>();
builder.Services.AddControllers();

builder.Services.AddGraphQLServer()

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

                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions();

builder.Services.AddScoped<ActorRepository, ActorRepository>();
builder.Services.AddScoped<MovieRepository, MovieRepository>();
builder.Services.AddScoped<RoleRepository, RoleRepository>();
builder.Services.AddScoped<ProducerRepository, ProducerRepository>();

var app = builder.Build();

Database.Init(app);

app.UseWebSockets();
app.UseRouting().UseEndpoints(endpoints => {
    endpoints.MapGraphQL();
    endpoints.MapControllers();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
