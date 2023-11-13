using fruitful.bll.Repositories;
using fruitful.bll.Services;
using Fruitful.BLL.Services;
using fruitful.dal.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("Fruitful_DB"));
builder.Services.AddSingleton<IMongoDatabase>(_ =>
{
    var settings = builder.Configuration.GetSection("Fruitful_DB").Get<DbSettings>();
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DbName);
});


builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton<EncryptionService>();
builder.Services.AddSingleton<JWTService>();


builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddPolicy(name: "FEOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
    }));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FEOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();