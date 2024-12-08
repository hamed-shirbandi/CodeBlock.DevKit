// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using System.Threading.RateLimiting;

namespace CodeBlock.DevKit.Web.Security;

internal class SecuritySettings
{
    public SecuritySettings()
    {
        RateLimiter = new();
        Enabled = true;
    }

    public bool Enabled { get; set; }
    public RateLimiterSettings RateLimiter { get; set; }

    internal static SecuritySettings CreateDefault()
    {
        return new SecuritySettings();
    }

    internal bool RateLimiterIsEnabled()
    {
        return Enabled && RateLimiter.Enabled;
    }
}

internal class RateLimiterSettings
{
    internal RateLimiterSettings()
    {
        Enabled = true;
        PermitLimit = 100;
        WindowSeconds = 60;
        QueueLimit = 0;
        QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    }

    public bool Enabled { get; set; }
    public int PermitLimit { get; set; }
    public int WindowSeconds { get; set; }
    public int QueueLimit { get; set; }
    public QueueProcessingOrder QueueProcessingOrder { get; set; }
}
