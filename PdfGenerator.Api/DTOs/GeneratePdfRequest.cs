namespace PdfGenerator.Api.DTOs;

public class GeneratePdfRequest
{
    public string HtmlContent { get; set; } = string.Empty;

    public string PageSize { get; set; } = "A4";

    public string Orientation { get; set; } = "Portrait";

    public string MarginTop { get; set; } = "20px";
    public string MarginBottom { get; set; } = "20px";
    public string MarginLeft { get; set; } = "20px";
    public string MarginRight { get; set; } = "20px";

    public bool PrintBackground { get; set; } = true;
}
