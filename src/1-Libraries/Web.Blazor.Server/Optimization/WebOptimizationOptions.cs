namespace CodeBlock.DevKit.Web.Blazor.Server.Optimization;

public class WebOptimizationOptions
{
    public WebOptimizationOptions()
    {
        Framework = new();
        App = new();
    }

    public bool Enabled { get; set; }
    public bool EnableCaching { get; set; }
    public bool EnableMemoryCache { get; set; }
    public bool EnableDiskCache { get; set; }
    public bool AllowEmptyBundle { get; set; }

    public OptimizationTarget Framework { get; set; }
    public OptimizationTarget App { get; set; }
}

public class OptimizationTarget
{
    public OptimizationTarget()
    {
        BundledCssFiles = [];
        BundledJsFiles = [];
    }

    public IEnumerable<OptimizationBundleModel> BundledCssFiles { get; set; }
    public IEnumerable<OptimizationBundleModel> BundledJsFiles { get; set; }
}

public class OptimizationBundleModel
{
    public string BundledFile { get; init; }
    public string[] FilesToBundle { get; set; }
}
