using GitHubAntiCorruptionApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using Swashbuckle.AspNetCore.Annotations;

namespace GitHubAntiCorruptionApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GithubWebHookController : ControllerBase
{
    private readonly IGitHubService _gitHubService;

    private readonly ILogger<GithubWebHookController> _logger;

    public GithubWebHookController(ILogger<GithubWebHookController> logger,
        IGitHubService gitHubService)
    {
        _logger = logger;
        _gitHubService = gitHubService;
    }

    [SwaggerOperation(Description = "Get repos hook by owner and repo")]
    [SwaggerResponse(StatusCodes.Status200OK, "Result sucess object", typeof(IReadOnlyList<RepositoryHook>))]
    [HttpGet("hooks/{owner}/{repo}")]
    public async Task<IReadOnlyList<RepositoryHook>> Get(string owner, string repo)
    {
        _logger.LogInformation("Get Repository hooks");
        return await _gitHubService.ListRepositoryHooks(owner, repo);
    }

    [SwaggerOperation(Description = "Edit repos hook by owner, repo and hookId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Result sucess object", typeof(RepositoryHook))]
    [HttpPut("hook/{owner}/{repo}/{hookId}")]
    public async Task<RepositoryHook> Put(string owner, string repo,
        int hookId, [FromBody] EditRepositoryHook editHook)
    {
        _logger.LogInformation("Update Repository Hook");
        return await _gitHubService.UpdateRepositoryHook(owner, repo, hookId, editHook);
    }
}
