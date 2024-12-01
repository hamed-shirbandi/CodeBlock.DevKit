using MediatR;

namespace CodeBlock.DevKit.Application.Commands;

public abstract class BaseCommand : IRequest<CommandResult> { }
