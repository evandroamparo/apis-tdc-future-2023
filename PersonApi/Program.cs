using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PersonApi.Data;
using PersonApi.Models;
using PersonApi.Repository;
using PersonApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureServices(builder.Services);


builder.Services.AddControllers();

builder.Services
    .AddApiVersioning(options =>
    {
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
        options.ReportApiVersions = true;
        options.DefaultApiVersion = ApiVersion.Default;
        options.AssumeDefaultVersionWhenUnspecified = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    })
    .AddMvc();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0",
        Title = "Person API",
        Description = "A simple example ASP.NET Core Web API",
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "2.0",
        Title = "Person API v2",
        Description = "A simple example ASP.NET Core Web API",
    });
});

var app = builder.Build();
InitializeDatabase(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant()
                    );
                }
            }
            );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<PersonDbContext>(options =>
        options.UseInMemoryDatabase("PersonDatabase"));

    services.AddScoped<IPersonRepository, PersonRepository>();
    services.AddScoped<IPersonService, PersonService>();

    services.AddControllers();
}


static void InitializeDatabase(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var dbContext = services.GetRequiredService<PersonDbContext>();
            InitializePersonDatabase(dbContext);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing database: {ex.Message}");
        }
    }
}

static void InitializePersonDatabase(PersonDbContext dbContext)
{
    if (!dbContext.Persons.Any())
    {
        var random = new Random();
        var names = new List<string> { "Alice", "Bob", "Charlie", "David", "Eva" };

        foreach (var name in names)
        {
            dbContext.Persons.Add(new Person(
                name,
                birthDate: DateOnly.FromDateTime(DateTime.Now.AddYears(-random.Next(20, 40)))));
        }

        dbContext.SaveChanges();
    }
}
