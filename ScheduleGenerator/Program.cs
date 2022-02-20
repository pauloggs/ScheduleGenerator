using Microsoft.AspNetCore.Mvc;
using ScheduleGenerator.Model.Input;
using ScheduleGenerator.Services;
using Carter;
using ScheduleGenerator.Model.Output;
using ScheduleGenerator.Model.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddCarter();

// FLuentValidation support for dotnet6 isn't ready:
// https://github.com/FluentValidation/FluentValidation/issues/1652
////builder.Services.AddSingleton
////    <IValidationAttributeAdapterProvider, CustomValidationAttributeAdapterProvider>();
////builder.AddSingleton<IValidator<QueryStudentHobbiesDto>, QueryStudentHobbiesDtoValidator>();
//builder.Services.AddSingleton<AbstractValidator<RecipeTrayStarts>, RecipeTrayStartsValidator>();

var BaseUrl = builder.Configuration.GetSection("BaseUrl").Value;

var app = builder.Build();

app.MapCarter();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/schedules", async ([FromBody] RecipeTrayStarts recipeTrayStarts) =>
{
    var converterService = new ConverterService();

    converterService.ValidateRecipeTrayStarts(recipeTrayStarts);

    var newHttpClient = new HttpClient() { BaseAddress = new Uri(BaseUrl) };
    var httpService = new HttpService(newHttpClient);
    
    var processorService = new ProcessorService();

    var rawRecipeData = await httpService.GetRecipeData();

    var recipes = converterService.GetRecipies(rawRecipeData);

    var towerSchedule = processorService.Process(recipes, recipeTrayStarts);

    return towerSchedule;
});

app.Run();
