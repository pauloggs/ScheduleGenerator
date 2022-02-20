using Microsoft.AspNetCore.Mvc;
using ScheduleGenerator.Model.Input;
using ScheduleGenerator.Services;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ConverterService>();
builder.Services.AddScoped<ProcessorService>();
builder.Services.AddCarter();

var BaseUrl = builder.Configuration.GetSection("BaseUrl").Value;

var app = builder.Build();

app.MapCarter();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/schedules", async (HttpContext context, [FromBody] RecipeTrayStarts recipeTrayStarts) =>
{
    try
    {
        // Ensure that posted RecipeTrayStarts request is valid via. FluentValidations
        context.RequestServices.GetRequiredService<ConverterService>().ValidateRecipeTrayStarts(recipeTrayStarts);

        // Retrieve list of Recipes from the RecipeApi
        var newHttpClient = new HttpClient() { BaseAddress = new Uri(BaseUrl) };
        var httpService = new HttpService(newHttpClient);
        var rawRecipeData = await httpService.GetRecipeData();

        // Deserialise to List<Recipe> model
        var recipes = context.RequestServices.GetRequiredService<ConverterService>().GetRecipies(rawRecipeData);

        // Process each RecipeTrayStart object against the corresponding recipe to add to the Commands for the tower. 
        var towerSchedule = context.RequestServices.GetRequiredService<ProcessorService>().Process(recipes, recipeTrayStarts);

        return Results.Ok(towerSchedule);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }    
});

app.Run();
