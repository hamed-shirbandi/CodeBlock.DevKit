// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Web.Api.JwtAuthentication;

public class JwtAuthenticationOptions
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public int ExpireDays { get; set; }
}

