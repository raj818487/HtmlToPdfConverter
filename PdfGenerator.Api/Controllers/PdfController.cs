using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Api.DTOs;
using PdfGenerator.Api.Services;

namespace PdfGenerator.Api.Controllers;

[ApiController]
[Route("api/pdf")]
public class PdfController(IPdfService pdfService) : ControllerBase
{
    [HttpPost("generate")]
    public async Task<IActionResult> GeneratePdf([FromBody] GeneratePdfRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.HtmlContent))
        {
            return BadRequest("HTML content is required.");
        }

        var pdfBytes = await pdfService.GeneratePdfAsync(request, cancellationToken);
        return File(pdfBytes, "application/pdf", "generated-report.pdf");
    }
}
