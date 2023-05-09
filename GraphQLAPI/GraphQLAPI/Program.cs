using FirebaseAdmin;
using FirebaseAdminAuthentication.DependencyInjection.Extensions;
using FirebaseAdminAuthentication.DependencyInjection.Models;
using GraphQLAPI.DataLoader;
using GraphQLAPI.Models.Internal;
using GraphQLAPI.Schema.Mutations;
using GraphQLAPI.Schema.Queries;
using GraphQLAPI.Schema.Queries.Courses;
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
    .AddType<CourseType>()
    .AddType<InterfaceType>()
    .AddTypeExtension<CourseQuery>()
    .AddTypeExtension<InstructorQuery>()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddInMemorySubscriptions()
    .AddAuthorization();

builder.Services.AddSingleton(FirebaseApp.Create());
builder.Services.AddFirebaseAuthentication();
builder.Services.AddAuthorization(o => o.AddPolicy("IsAdmin", p => p.RequireClaim(FirebaseUserClaimType.EMAIL, "behdad.test@gmail.com")));

builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<InstructorRepository>();
builder.Services.AddScoped<InstructorDataLoader>();
builder.Services.AddScoped<UserDataLoader>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
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
