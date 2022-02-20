using Microsoft.AspNetCore.Mvc;
using ScheduleGenerator.Model.Input;
using ScheduleGenerator.Services;
using MiminalApis.Validators;
using ScheduleGenerator.Model.Validators;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var BaseUrl = builder.Configuration.GetSection("BaseUrl").Value;

var app = builder.Build();


// FLuentValidation support for dotnet6 isn't ready:
// https://github.com/FluentValidation/FluentValidation/issues/1652

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/schedules", async ([FromBody] RecipeTrayStarts recipeTrayStarts) =>
{
    var newHttpClient = new HttpClient() { BaseAddress = new Uri(BaseUrl) };

    var httpService = new HttpService(newHttpClient);

    var rawRecipeData = await httpService.GetRecipeData();

    var converterService = new ConverterService();

    var recipes = converterService.GetRecipies(rawRecipeData);

    var processorService = new ProcessorService();

    var towerSchedule = processorService.Process(recipes, recipeTrayStarts);

    var jsonSerializerSettings = new JsonSerializerSettings();
    jsonSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    return JsonConvert.SerializeObject(towerSchedule, jsonSerializerSettings);
});

app.Run();
