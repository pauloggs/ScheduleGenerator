using ScheduleGenerator.Services;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var BaseUrl = builder.Configuration.GetSection("BaseUrl").Value;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        opt.RoutePrefix = string.Empty;
    });
}

app.MapGet("/generate", async () => 
{
    var newHttpClient = new HttpClient() { BaseAddress = new Uri(BaseUrl) };

    var httpService = new HttpService(newHttpClient);

    var response = await httpService.GetRecipeData();

    return response;
});

app.Run();
