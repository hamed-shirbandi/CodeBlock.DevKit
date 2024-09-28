namespace CodeBlock.DevKit.Web.Optimization;

public class WebOptimizerOptions
{
    public bool Enabled { get; set; }
    public string BundledCssOutputFile { get; set; }
    public string BundledJsOutputFile { get; set; }
    public string[] CssFilesToBundle { get; set; }
    public string[] JsFilesToBundle { get; set; }
}
