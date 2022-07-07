using Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApiDbContext>();

builder.Services.AddControllers();
builder.Services.AddGraphQLServer()
                .AddInMemorySubscriptions()
                .AddQueryType<Api.Resolvers.Query>()
                .AddMutationType<Api.Resolvers.Mutation>()
                .AddSubscriptionType<Api.Resolvers.Subscription>();

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
