using Common.Extensions;
using GraphQLAPI.Schema.Mutations;
using GraphQLAPI.Schema.Queries;
using GraphQLAPI.Schema.Subscriptions;
using GraphQLAPI.Services.Courses;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddSqlDatabase();
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutations>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();

builder.Services.AddScoped<CoursesRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();
app.UseWebSockets();
app.MapGraphQL();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMvc();

app.Run();
