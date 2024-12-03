// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Web.Blazor.Server.Optimization;

public class WebOptimizationOptions
{
    public WebOptimizationOptions()
    {
        BundledCssFiles = [];
        BundledJsFiles = [];
    }

    public bool Enabled { get; set; }
    public bool EnableCaching { get; set; }
    public bool EnableMemoryCache { get; set; }
    public bool EnableDiskCache { get; set; }
    public bool AllowEmptyBundle { get; set; }

    public IEnumerable<OptimizationBundleModel> BundledCssFiles { get; set; }
    public IEnumerable<OptimizationBundleModel> BundledJsFiles { get; set; }
}

public class OptimizationBundleModel
{
    public string BundledFile { get; init; }
    public string[] FilesToBundle { get; set; }
}
