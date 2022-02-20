using ScheduleGenerator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var BaseUrl = builder.Configuration.GetSection("BaseUrl").Value;

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

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
