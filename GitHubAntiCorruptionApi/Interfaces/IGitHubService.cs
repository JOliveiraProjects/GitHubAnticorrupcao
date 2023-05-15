using Octokit;

namespace GitHubAntiCorruptionApi.Interfaces;

public interface IGitHubService
{
    Task<Repository> CreateRepository(NewRepository newRepository);
    Task<IReadOnlyList<Branch>> ListBranches(string owner, string repo);
    Task<IReadOnlyList<RepositoryHook>> ListRepositoryHooks(string owner, string repo);
    Task<RepositoryHook> UpdateRepositoryHook(string owner, string repo, int hookId, EditRepositoryHook editHook);
}