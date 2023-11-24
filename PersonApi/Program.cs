using Microsoft.EntityFrameworkCore;
using PersonApi.Data;
using PersonApi.Models;
using PersonApi.Repository;
using PersonApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureServices(builder.Services);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
InitializeDatabase(app);

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
            dbContext.Persons.Add(new Person(name));
        }

        dbContext.SaveChanges();
    }
}
