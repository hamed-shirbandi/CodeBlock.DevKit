// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Infrastructure.Services;

public class EnvironmentService
{
    private readonly string _environment;

    public EnvironmentService(string environment)
    {
        _environment = environment;
    }

    public string GetEnvironmentName() => _environment;

    public bool IsDevelopment() => _environment == "Development";

    public bool IsProduction() => _environment == "Production";

    public bool IsStaging() => _environment == "Staging";
}
