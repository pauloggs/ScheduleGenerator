using ScheduleGenerator.Services;
using System.Net.Http.Headers;
using Newtonsoft;
using Newtonsoft.Json.Linq;

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

    // TODO.
    // call to get recipe data as a string (HttpService)
    // deserialize this string as a List<Recipe> object (ConverterService)
    // process this List<Recipe> object into a TowerSchedule object
    // return the TowerSchedule object

    var rawRecipeData = await httpService.GetRecipeData();

    var converterService = new ConverterService();

    var recipies = converterService.GetRecipies(rawRecipeData);

    return rawRecipeData;
});

app.Run();
