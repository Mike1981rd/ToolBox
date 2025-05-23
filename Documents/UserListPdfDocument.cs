using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ToolBox.Documents
{
    // DTO para pasar datos al documento PDF
    public class UserPdfDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

    public class UserListPdfDocument : IDocument
    {
        public List<UserPdfDto> Users { get; }

        public UserListPdfDocument(List<UserPdfDto> users)
        {
            Users = users;
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
                    column.Item().Text("Lista de Usuarios")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);
                    column.Item().Text(DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                        .Light().FontSize(9);
                });
            });
        }

        void ComposeContent(IContainer container)
        {
            container.Table(table =>
            {
                // Definir columnas
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3); // Nombre Completo
                    columns.RelativeColumn(4); // Email
                    columns.RelativeColumn(2); // Rol
                    columns.RelativeColumn(2); // Estado
                });

                // Cabecera de la tabla
                table.Header(header =>
                {
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Nombre Completo").SemiBold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Email").SemiBold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Rol").SemiBold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Estado").SemiBold();
                });

                // Filas de datos
                foreach (var user in Users)
                {
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(user.FullName);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(user.Email);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(user.RoleName);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(user.Status);
                }
            });
        }
    }
}