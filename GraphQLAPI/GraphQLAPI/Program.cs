using GraphQLAPI.DataLoader;
using GraphQLAPI.Models.Internal;
using GraphQLAPI.Schema.Mutations;
using GraphQLAPI.Schema.Queries;
using GraphQLAPI.Schema.Subscriptions;
using GraphQLAPI.Services.Courses;
using SQL.Database;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var configuration = builder.Configuration;
var sqlConnection = configuration?.GetSection(nameof(SqlSettings)).Get<SqlSettings>();
builder.Services.AddSingleton<IDatabaseFactory>(new DatabaseFactory(sqlConnection.ConnectionString));

builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutations>()
    .AddSubscriptionType<Subscription>()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions();

builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<InstructorRepository>();
builder.Services.AddScoped<InstructorDataLoader>();


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
