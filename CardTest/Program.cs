using CardTest.Communicator.HackerNews;
using CardTest.HostedServices;
using CardTest.Options;
using CardTest.Repositories;
using CardTest.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddMemoryCache();
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Back ground Job to load and cache data in advance
builder.Services.AddHostedService<HackerNewsCacheWorker>();

//Application Options
builder.Services.Configure<ApplicationOptions>((settings) =>
{
    builder.Configuration.GetSection("ApplicationOptions").Bind(settings);
});

//Services
builder.Services.AddSingleton<IStoryService, StoryService>();

//Repository
builder.Services.AddSingleton<IStoryRepository, StoryRepository>();

//Client
builder.Services.AddHttpClient<IHackerNewsClient,HackerNewsClient>();
builder.Services.Configure<HackerNewsOptions>((settings) =>
{
    builder.Configuration.GetSection("HackerNewsOptions").Bind(settings);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
