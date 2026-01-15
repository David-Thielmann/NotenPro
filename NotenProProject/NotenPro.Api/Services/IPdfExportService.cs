using NotenPro.Shared.DTOs;

namespace HTLKrems.GradeManagement.Api.Services;

/// <summary>
/// Creates PDF exports for grade lists.
/// </summary>
public interface IPdfExportService
{
    byte[] CreateGradesPdf(IEnumerable<GradeDto> grades);
}
