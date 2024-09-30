using BlazorServerApp.Infrastructure;
using BlazorServerApp.Models;
using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;

namespace BlazorServerApp.UserCases.RegisterUser;

public class RegisterUserUseCase : BaseCommandHandler, IRequestHandler<RegisterUserRequest, CommandResult>
{
    private readonly Database _database;

    public RegisterUserUseCase(IInMemoryBus inMemoryBus, Database database)
        : base(inMemoryBus)
    {
        _database = database;
    }

    public async Task<CommandResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var user = new User(request.UserName);

        _database.AddUser(user);

        return CommandResult.Create(entityId: user.Id);
    }
}
