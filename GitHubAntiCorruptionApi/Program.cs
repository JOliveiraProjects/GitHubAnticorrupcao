using GitHubAntiCorruptionApi.Interfaces;
using GitHubAntiCorruptionApi.Services;
using Octokit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGitHubService, GitHubService>();
builder.Services.AddScoped<IGitHubClient>(sp =>
{
    var gitHubHeader = builder.Configuration["GitHub:Header"];
    var gitHubToken = builder.Configuration["GitHub:Token"];
    var gitHubClient = new GitHubClient(new ProductHeaderValue(gitHubHeader))
    {
        Credentials = new Credentials(gitHubToken)
    };
    return gitHubClient;
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();
app.UseCors(builder => builder
    .SetIsOriginAllowed(_ => true)
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
