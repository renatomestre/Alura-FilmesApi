using FilmesApi.Data;
using FilmesApi.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMvc();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Filmes API",
        Version = "v1"
    });
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<FilmeProfile>();
});

builder.Services.AddDbContext<FilmeContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("FilmeConnection"))
);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();