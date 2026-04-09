using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using System.Text.Json.Serialization;
using User.API.Filters;
using User.Application.Interfaces;
using User.Application.Validators.UserValidation;
using User.Infra.Data;
using User.Infra.Services;
using User.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Validation Filter + Enum como string
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter()
    );
});

QuestPDF.Settings.License = LicenseType.Community;

// Desativa o retorno padrão do ApiController
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gerenciamento de usuários",
        Version = "1.0",
        Contact = new OpenApiContact
        {
            Name = "Patrick",
            Email = "Mendespatrick720@gmail.com"
        }
    });

    // XML da API
    var apiXmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
    var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
    options.IncludeXmlComments(apiXmlPath);

    // XML da Application (DTOs)
    var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, "User.Application.xml");
    options.IncludeXmlComments(applicationXmlPath);
});

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// DbContext
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<ISuporteRepository, SuporteRepository>();
builder.Services.AddScoped<ITransferenciaService, TransferenciaService>();
builder.Services.AddScoped<IPdfGenerator, ComprovantePdfGeneratorService>();

// CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware global de erro
app.UseMiddleware<ExceptionMiddleware>();


// CORS
app.UseRouting();
app.UseCors("AngularPolicy");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();