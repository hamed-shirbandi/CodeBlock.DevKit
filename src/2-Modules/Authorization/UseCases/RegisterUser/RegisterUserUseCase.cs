using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Domain.Services;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.RegisterUser;

public class RegisterUserUseCase : BaseCommandHandler, IRequestHandler<RegisterUserRequest, CommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptionService _encryptionService;

    public RegisterUserUseCase(IUserRepository userRepository, IEncryptionService encryptionService, IInMemoryBus inMemoryBus)
        : base(inMemoryBus)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
    }

    public async Task<CommandResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var user = User.Register(_userRepository, _encryptionService, request.Mobile, request.Email, request.Mobile);

        await _userRepository.AddAsync(user);

        return CommandResult.Create(entityId: user.Id);
    }
}
