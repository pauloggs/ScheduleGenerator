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
    var response = "Nothing";

    try
    {
        using var client = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl)
        };

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var responseMessage = client.GetAsync("recipe").Result;


        if (responseMessage.IsSuccessStatusCode)
        {
            response = await responseMessage.Content.ReadAsStringAsync();
        }

    }
    catch (Exception ex)
    {
        response = ex.Message;
    }

    return response;
});

app.Run();
