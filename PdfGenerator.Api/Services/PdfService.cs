using Microsoft.Playwright;
using PdfGenerator.Api.DTOs;

namespace PdfGenerator.Api.Services;

public class PdfService : IPdfService
{
    private static readonly HashSet<string> AllowedFormats = new(StringComparer.OrdinalIgnoreCase)
    {
        "A3", "A4", "A5", "Letter", "Legal", "Tabloid", "Ledger"
    };

    public async Task<byte[]> GeneratePdfAsync(GeneratePdfRequest request, CancellationToken cancellationToken = default)
    {
        var sanitizedHtml = SanitizeHtml(request.HtmlContent);
        var finalHtml = BuildHtmlDocument(sanitizedHtml);

        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = true
            });

        var page = await browser.NewPageAsync();

        await page.SetContentAsync(finalHtml, new PageSetContentOptions
        {
            WaitUntil = WaitUntilState.NetworkIdle
        });

        var format = AllowedFormats.Contains(request.PageSize) ? request.PageSize : "A4";
        var isLandscape = request.Orientation.Equals("Landscape", StringComparison.OrdinalIgnoreCase);

        return await page.PdfAsync(new PagePdfOptions
        {
            Format = format,
            Landscape = isLandscape,
            PrintBackground = request.PrintBackground,
            Margin = new Margin
            {
                Top = request.MarginTop,
                Bottom = request.MarginBottom,
                Left = request.MarginLeft,
                Right = request.MarginRight
            }
        });
    }

    private static string BuildHtmlDocument(string bodyHtml)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"" />
    <style>
        * {{ box-sizing: border-box; }}
        body {{
            font-family: Arial, sans-serif;
            font-size: 13px;
            color: #000;
            margin: 0;
            padding: 0;
        }}
        .pdf-container {{ width: 100%; }}
    </style>
</head>
<body>
    <div class=""pdf-container"">
        {bodyHtml}
    </div>
</body>
</html>
";
    }

    private static string SanitizeHtml(string htmlContent)
    {
        return htmlContent
            .Replace("<script", "&lt;script", StringComparison.OrdinalIgnoreCase)
            .Replace("</script>", "&lt;/script&gt;", StringComparison.OrdinalIgnoreCase);
    }
}
