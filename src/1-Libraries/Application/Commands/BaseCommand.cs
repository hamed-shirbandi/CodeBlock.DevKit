// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using MediatR;

namespace CodeBlock.DevKit.Application.Commands;

public abstract class BaseCommand : IRequest<CommandResult> { }

