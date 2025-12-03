using NotenPro.Api.DTOs;
public interface IPdfExportService
{
    byte[] CreateGradesPdf(IEnumerable<GradeDto> grades);
}