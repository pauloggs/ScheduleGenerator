using Microsoft.AspNetCore.Mvc;
using ScheduleGenerator.Model.Input;
using ScheduleGenerator.Services;
using MiminalApis.Validators;
using ScheduleGenerator.Model.Validators;

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

    // TODO.
    // call to get recipe data as a string (HttpService) DONE
    // deserialize this string as a List<Recipe> object (ConverterService) DONE
    // process this List<Recipe> object into a TowerSchedule object
    // return the TowerSchedule object

    var rawRecipeData = await httpService.GetRecipeData();

    var converterService = new ConverterService();

    var recipies = converterService.GetRecipies(rawRecipeData);

    return rawRecipeData;
});

app.Run();
