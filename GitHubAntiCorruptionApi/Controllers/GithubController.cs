using GitHubAntiCorruptionApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace GitHubAntiCorruptionApi.Controllers;

[SwaggerTag("Get Repos GitHub")]
[Consumes("application/json")]
[Produces("application/json")]
[ApiController]
[Route("[controller]")]
public class GithubController : ControllerBase
{
    private readonly IGitHubService _gitHubService;

    private readonly ILogger<GithubController> _logger;

    public GithubController(ILogger<GithubController> logger,
        IGitHubService gitHubService)
    {
        _logger = logger;
        _gitHubService = gitHubService;
    }

    [SwaggerOperation(Description = "Get Branches by owner and repo")]
    [SwaggerResponse(StatusCodes.Status200OK, "Result sucess object", typeof(IReadOnlyList<Branch>))]
    [HttpGet("branches/{owner}/{repo}")]
    public async Task<IReadOnlyList<Branch>> Get(string owner, string repo)
    {
        _logger.LogInformation("Get Branches");
        return await _gitHubService.ListBranches(owner, repo);
    }

    [SwaggerOperation(Description = "Create new repo")]
    [SwaggerResponse(StatusCodes.Status200OK, "Result sucess object", typeof(Repository))]
    [HttpPost("repos")]
    public async Task<Repository> Post([FromBody] NewRepository repository)
    {
        _logger.LogInformation("Create Repository");
        return await _gitHubService.CreateRepository(repository);
    }
}
