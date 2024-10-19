namespace CodeBlock.DevKit.Web.Optimization;

public class WebOptimizerOptions
{
    public WebOptimizerOptions()
    {
        Framework = new();
        App = new();
    }

    public bool Enabled { get; set; }
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
