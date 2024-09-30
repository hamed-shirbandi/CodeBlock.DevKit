namespace CodeBlock.DevKit.Web.Optimization;

public class WebOptimizerOptions
{
    public WebOptimizerOptions()
    {
        CssFilesToBundle = [];
        JsFilesToBundle = [];
    }

    public bool Enabled { get; set; }
    public string BundledCssOutputFile { get; set; }
    public string BundledJsOutputFile { get; set; }
    public string[] CssFilesToBundle { get; set; }
    public string[] JsFilesToBundle { get; set; }

    public bool ShouldBundleCssFiles()
    {
        return !string.IsNullOrEmpty(BundledCssOutputFile) && CssFilesToBundle.Length > 0;
    }

    public bool ShouldBundleJsFiles()
    {
        return !string.IsNullOrEmpty(BundledJsOutputFile) && JsFilesToBundle.Length > 0;
    }
}
