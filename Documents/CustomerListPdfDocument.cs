using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ToolBox.Documents
{
    // DTO para pasar datos al documento PDF
    public class CustomerPdfDto
    {
        public string CustomerNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }

    public class CustomerListPdfDocument : IDocument
    {
        public List<CustomerPdfDto> Customers { get; }

        public CustomerListPdfDocument(List<CustomerPdfDto> customers)
        {
            Customers = customers;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(50);
                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text("Lista de Clientes")
                        .FontSize(20).FontColor(Colors.Blue.Medium).Bold();

                    column.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(12).FontColor(Colors.Grey.Darken2);
                });

                row.ConstantItem(100).Height(50).Placeholder();
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);

                column.Item().Element(ComposeTable);

                if (!Customers.Any())
                {
                    column.Item().PaddingVertical(10).Element(ComposeEmptyState);
                }
            });
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                // Definir columnas
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(60);   // Número Cliente
                    columns.RelativeColumn(2);    // Nombre
                    columns.RelativeColumn(2);    // Email
                    columns.RelativeColumn(1);    // Teléfono
                    columns.RelativeColumn(1.5f); // Empresa
                    columns.ConstantColumn(50);   // País
                    columns.ConstantColumn(60);   // Estado
                    columns.ConstantColumn(70);   // Fecha
                });

                // Cabecera
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("N° Cliente").FontSize(10).Bold();
                    header.Cell().Element(CellStyle).Text("Nombre Completo").FontSize(10).Bold();
                    header.Cell().Element(CellStyle).Text("Email").FontSize(10).Bold();
                    header.Cell().Element(CellStyle).Text("Teléfono").FontSize(10).Bold();
                    header.Cell().Element(CellStyle).Text("Empresa").FontSize(10).Bold();
                    header.Cell().Element(CellStyle).Text("País").FontSize(10).Bold();
                    header.Cell().Element(CellStyle).Text("Estado").FontSize(10).Bold();
                    header.Cell().Element(CellStyle).Text("Fecha").FontSize(10).Bold();

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // Filas de datos
                foreach (var customer in Customers)
                {
                    table.Cell().Element(CellStyle).Text(customer.CustomerNumber).FontSize(9);
                    table.Cell().Element(CellStyle).Text(customer.FullName).FontSize(9);
                    table.Cell().Element(CellStyle).Text(customer.Email).FontSize(8);
                    table.Cell().Element(CellStyle).Text(customer.PhoneNumber ?? "-").FontSize(9);
                    table.Cell().Element(CellStyle).Text(customer.CompanyName ?? "-").FontSize(8);
                    table.Cell().Element(CellStyle).Text(customer.Country ?? "-").FontSize(9);
                    table.Cell().Element(CellStyle)
                        .Text(customer.Status)
                        .FontSize(9)
                        .FontColor(customer.Status == "Activo" ? Colors.Green.Darken2 : Colors.Orange.Darken2);
                    table.Cell().Element(CellStyle).Text(customer.CreatedAt).FontSize(8);

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(3);
                    }
                }
            });
        }

        void ComposeEmptyState(IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {
                column.Item().AlignCenter().Text("No se encontraron clientes")
                    .FontSize(16).FontColor(Colors.Grey.Darken1);
            });
        }

        public byte[] GeneratePdf()
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
            }).GeneratePdf();
        }
    }
}