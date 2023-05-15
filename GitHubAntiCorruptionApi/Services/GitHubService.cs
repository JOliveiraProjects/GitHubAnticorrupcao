using GitHubAntiCorruptionApi.Interfaces;
using Octokit;

namespace GitHubAntiCorruptionApi.Services;

public class GitHubService : IGitHubService
{
  private readonly IGitHubClient _gitHubClient;

  public GitHubService(IGitHubClient gitHubClient)
  {
    _gitHubClient = gitHubClient;
  }

  public async Task<Repository> CreateRepository(NewRepository newRepository)
  {
    return await _gitHubClient.Repository.Create(newRepository);
  }

  public async Task<IReadOnlyList<Branch>> ListBranches(string owner, string repo)
  {
    return await _gitHubClient.Repository.Branch.GetAll(owner, repo);
  }

  public async Task<IReadOnlyList<RepositoryHook>> ListRepositoryHooks(string owner, string repo)
  {
    return await _gitHubClient.Repository.Hooks.GetAll(owner, repo);
  }

  public async Task<RepositoryHook> UpdateRepositoryHook(string owner, string repo, int hookId, EditRepositoryHook editHook)
  {
    return await _gitHubClient.Repository.Hooks.Edit(owner, repo, hookId, editHook);
  }
}