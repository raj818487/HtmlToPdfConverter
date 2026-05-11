using PdfGenerator.Api.DTOs;

namespace PdfGenerator.Api.Services;

public interface IPdfService
{
    Task<byte[]> GeneratePdfAsync(GeneratePdfRequest request, CancellationToken cancellationToken = default);
}
