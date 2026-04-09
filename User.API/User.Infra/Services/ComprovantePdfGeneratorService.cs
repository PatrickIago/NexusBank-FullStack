using QuestPDF.Fluent;
using User.Application.Dtos.TransferenciasDto;
using User.Application.Interfaces;
namespace User.Infra.Services;

public class ComprovantePdfGeneratorService : IPdfGenerator
{
    public byte[] GerarPdf(ComprovanteTransferenciaDto comprovante)
    {
        var pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);

                page.Header()
                    .Text("Comprovante de Transferência")
                    .FontSize(20)
                    .Bold()
                    .AlignCenter();

                page.Content().Column(col =>
                {
                    col.Item().Text($"Transação: {comprovante.TransacaoId}");
                    col.Item().Text($"Remetente: {comprovante.Remetente}");
                    col.Item().Text($"Destinatário: {comprovante.Destinatario}");
                    col.Item().Text($"Remetente: {comprovante.DataHora}");
                    col.Item().Text($"Valor: R$ {comprovante.Valor:N2}");
                });
            });
        });

        return pdf.GeneratePdf();
    }
}
