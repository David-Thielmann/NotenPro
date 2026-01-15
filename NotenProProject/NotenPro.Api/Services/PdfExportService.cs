using System;
using System.Collections.Generic;
using System.Linq;
using NotenPro.Shared.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace HTLKrems.GradeManagement.Api.Services
{
    public class PdfExportService : IPdfExportService
    {
        public byte[] CreateGradesPdf(IEnumerable<GradeDto> grades)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var gradeList = grades.ToList();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // ---------- HEADER ----------
                    page.Header().PaddingBottom(10).Row(row =>
                    {
                        row.RelativeItem().Text("Notenübersicht")
                            .FontSize(20)
                            .Bold()
                            .AlignLeft();

                        row.ConstantItem(120).AlignRight().Text("HTL Krems")
                            .FontSize(12);
                    });

                    // ---------- CONTENT ----------
                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        // Spalten: Fach, Test, Note, Punkte, Datum
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2); // Fach
                            columns.RelativeColumn(2); // Test
                            columns.RelativeColumn(1); // Note
                            columns.RelativeColumn(1); // Punkte
                            columns.RelativeColumn(1); // Datum
                        });

                        // Tabellenkopf
                        table.Header(header =>
                        {
                            header.Cell().Element(HeaderStyle).Text("Fach").Bold();
                            header.Cell().Element(HeaderStyle).Text("Test").Bold();
                            header.Cell().Element(HeaderStyle).Text("Note").Bold();
                            header.Cell().Element(HeaderStyle).Text("Punkte").Bold();
                            header.Cell().Element(HeaderStyle).Text("Datum").Bold();
                        });

                        // Tabellenzeilen
                        foreach (var g in gradeList)
                        {
                            table.Cell().Element(RowStyle).Text(g.Subject);
                            table.Cell().Element(RowStyle).Text(g.TestName);

                            table.Cell().Element(RowStyle)
                                .Text(g.GradeValue.ToString());

                            table.Cell().Element(RowStyle)
                                .Text(g.Points.HasValue && g.MaxPoints.HasValue
                                    ? $"{g.Points}/{g.MaxPoints}"
                                    : "-");

                            // Date kann DateTime, DateOnly oder string sein – ToString() geht immer
                            table.Cell().Element(RowStyle)
                                .Text(g.Date.ToString());
                        }

                        // ---------- STYLE HELPERS ----------
                        static IContainer HeaderStyle(IContainer container) =>
                            container
                                .PaddingVertical(5)
                                .Background(Colors.Grey.Lighten2);

                        static IContainer RowStyle(IContainer container) =>
                            container
                                .BorderBottom(1)
                                .BorderColor(Colors.Grey.Lighten3)
                                .PaddingVertical(4);
                    });

                    // ---------- FOOTER ----------
                    page.Footer()
                        .AlignCenter()
                        .Text($"Erstellt am: {DateTime.Now:dd.MM.yyyy HH:mm}")
                        .FontSize(10);
                });
            });

            return document.GeneratePdf();
        }
    }
}
