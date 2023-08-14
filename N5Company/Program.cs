using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using N5Company.Errors;
using N5Company.Kafka;
using N5Company.Persistence;
using N5Company.Repositories;
using Nest;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(o => o.Filters.Add<N5ExceptionHandlerAttribute>());

builder.Services.AddDbContext<DataContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var elasticsearchSettings = new ConnectionSettings(new Uri(builder.Configuration.GetConnectionString("ElasticsearchConnection")))
    .DefaultIndex("permission_index")
    .BasicAuthentication(builder.Configuration["ElasticsearchSettings:UserName"], builder.Configuration["ElasticsearchSettings:Password"])
    .ServerCertificateValidationCallback(CertificateValidations.AllowAll);

builder.Services.AddSingleton<IElasticClient>(new ElasticClient(elasticsearchSettings));
builder.Services.AddScoped<IDataContext, DataContext>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<ProblemDetailsFactory, N5ProblemDetailsFactory>();
builder.Services.AddSingleton<KafkaProducer>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(b =>
    {
        b.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
    });
});

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
