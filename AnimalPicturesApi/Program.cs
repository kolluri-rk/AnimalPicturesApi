using System.Text.Json.Serialization;
using AnimalPicturesApi.Repositories;
using AnimalPicturesApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IPictureFetchService, PictureFetchService>();
builder.Services.AddScoped<IAnimalsPicturesRepository, AnimalsPicturesRepository>();
builder.Services.AddHttpClient<IAnimalImageApiService, AnimalImageApiService>();

string mySqlConnectionStr = builder.Configuration.GetConnectionString("DBConn");
builder.Services.AddDbContextPool<AnimalsDbContext>(options => 
    options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));


builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();