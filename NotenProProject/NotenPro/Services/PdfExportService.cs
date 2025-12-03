using HTLKrems.GradeManagement.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace HTLKrems.GradeManagement.Services;

public interface IPdfExportService
{
    byte[] CreateGradesPdf(IEnumerable<Grade> grades);
}

public class PdfExportService : IPdfExportService
{
    public byte[] CreateGradesPdf(IEnumerable<Grade> grades)
    {
        var gradesList = grades.ToList();

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header().Text("Meine Noten")
                    .SemiBold().FontSize(18).FontColor(Colors.Blue.Darken2);

                page.Content().Table(table =>
                {
                    // Spalten
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(80);  // Fach
                        columns.ConstantColumn(90);  // Test
                        columns.ConstantColumn(40);  // Note
                        columns.ConstantColumn(70);  // Punkte
                        columns.ConstantColumn(80);  // Datum
                        columns.RelativeColumn();    // Lehrer
                    });

                    // Kopfzeile
                    table.Header(header =>
                    {
                        header.Cell().Element(CellHeader).Text("Fach");
                        header.Cell().Element(CellHeader).Text("Test");
                        header.Cell().Element(CellHeader).Text("Note");
                        header.Cell().Element(CellHeader).Text("Punkte");
                        header.Cell().Element(CellHeader).Text("Datum");
                        header.Cell().Element(CellHeader).Text("Lehrer");
                    });

                    // Zeilen
                    foreach (var g in gradesList)
                    {
                        table.Cell().Element(CellBody).Text(g.Subject);
                        table.Cell().Element(CellBody).Text(g.TestName);
                        table.Cell().Element(CellBody).Text(g.GradeValue.ToString("0.0"));
                        table.Cell().Element(CellBody).Text(
                            g.Points.HasValue && g.MaxPoints.HasValue
                                ? $"{g.Points}/{g.MaxPoints}"
                                : "-");
                        table.Cell().Element(CellBody).Text(g.Date.ToString("dd.MM.yyyy"));
                        table.Cell().Element(CellBody).Text(g.TeacherName);
                    }
                });

                page.Footer().AlignRight().Text($"Erstellt am {DateTime.Now:dd.MM.yyyy HH:mm}");
            });
        });

        return document.GeneratePdf();

        static IContainer CellHeader(IContainer container) =>
            container.PaddingVertical(5)
                     .DefaultTextStyle(x => x.SemiBold())
                     .BorderBottom(1)
                     .BorderColor(Colors.Grey.Darken2);

        static IContainer CellBody(IContainer container) =>
            container.PaddingVertical(3);
    }
}
