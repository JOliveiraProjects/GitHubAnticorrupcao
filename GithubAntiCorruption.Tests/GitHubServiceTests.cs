using GitHubAntiCorruptionApi.Services;
using Moq;
using Octokit;

namespace GithubAntiCorruptionLayer.Tests;

public class GitHubServiceTests
{
    [Fact]
    public async Task CreateRepository_CallsCreateOnGitHubClient()
    {
        // Arrange
        var mockGitHubClient = new Mock<IGitHubClient>();
        var repository = new Repository();
        var newRepository = new NewRepository("TestRepo");

        mockGitHubClient.Setup(client => client.Repository.Create(newRepository))
            .ReturnsAsync(repository)
            .Verifiable();

        var service = new GitHubService(mockGitHubClient.Object);

        // Act
        var result = await service.CreateRepository(newRepository);

        // Assert
        mockGitHubClient.Verify();
        Assert.Equal(repository, result);
    }

    [Fact]
    public async Task ListBranches_CallsGetAllOnGitHubClient()
    {
        // Arrange
        var mockGitHubClient = new Mock<IGitHubClient>();
        var branches = new List<Branch> { new Branch(), new Branch() };
        string owner = "testowner";
        string repo = "testrepo";

        mockGitHubClient.Setup(client => client.Repository.Branch.GetAll(owner, repo))
            .ReturnsAsync(branches)
            .Verifiable();

        var service = new GitHubService(mockGitHubClient.Object);

        // Act
        var result = await service.ListBranches(owner, repo);

        // Assert
        mockGitHubClient.Verify();
        Assert.Equal(branches, result);
    }

    [Fact]
    public async Task ListRepositoryHooks_CallsGetAllOnGitHubClient()
    {
        // Arrange
        var mockGitHubClient = new Mock<IGitHubClient>();
        var hooks = new List<RepositoryHook> { new RepositoryHook(), new RepositoryHook() };
        string owner = "testowner";
        string repo = "testrepo";

        mockGitHubClient.Setup(client => client.Repository.Hooks.GetAll(owner, repo))
            .ReturnsAsync(hooks)
            .Verifiable();

        var service = new GitHubService(mockGitHubClient.Object);

        // Act
        var result = await service.ListRepositoryHooks(owner, repo);

        // Assert
        mockGitHubClient.Verify();
        Assert.Equal(hooks, result);
    }

    [Fact]
    public async Task UpdateRepositoryHook_CallsEditOnGitHubClient()
    {
        // Arrange
        var mockGitHubClient = new Mock<IGitHubClient>();
        var hook = new RepositoryHook();
        string owner = "testowner";
        string repo = "testrepo";
        int hookId = 1;
        var editHook = new EditRepositoryHook();

        mockGitHubClient.Setup(client => client.Repository.Hooks.Edit(owner, repo, hookId, editHook))
            .ReturnsAsync(hook)
            .Verifiable();

        var service = new GitHubService(mockGitHubClient.Object);

        // Act
        var result = await service.UpdateRepositoryHook(owner, repo, hookId, editHook);

        // Assert
        mockGitHubClient.Verify();
        Assert.Equal(hook, result);
    }
}