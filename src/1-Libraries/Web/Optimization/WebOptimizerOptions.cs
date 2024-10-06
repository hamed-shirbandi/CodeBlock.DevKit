namespace CodeBlock.DevKit.Web.Optimization;

public class WebOptimizerOptions
{
    public WebOptimizerOptions()
    {
        BundledCssFiles = [];
        BundledJsFiles = [];
    }

    public bool Enabled { get; set; }

    public IEnumerable<BundleModel> BundledCssFiles { get; set; }
    public IEnumerable<BundleModel> BundledJsFiles { get; set; }
}

public class BundleModel
{
    public string BundledFile { get; init; }
    public string[] FilesToBundle { get; set; }
}
