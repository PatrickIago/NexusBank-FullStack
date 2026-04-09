using User.Domain.Enuns;
namespace User.Domain.Entities;

public class Transacao
{
    public Guid Id { get; set; }
    public decimal Valor { get; private set; }
    public DateTime DataHora { get; private set; } = DateTime.UtcNow;
    public StatusTransacao Status { get; private set; }


    // Chave enviada pelo front-end para evitar cobrança duplicada
    public Guid ChaveIdempotencia { get; private set; }

    // Relacionamentos

    public int CarteiraRemetenteId { get; private set; }
    public Carteira CarteiraRemetente { get; private set; }

    public int CarteiraDestinatarioId { get; private set; }
    public Carteira CarteiraDestinatario { get; private set; }

    // Construtores
    public Transacao() { }

    public Transacao(int carteiraRemetenteId, int carteiraDestinatarioId, decimal valor, Guid chaveIdempotencia)
    {
        Id = Guid.NewGuid();
        CarteiraRemetenteId = carteiraRemetenteId;
        CarteiraDestinatarioId = carteiraDestinatarioId;
        Valor = valor;
        DataHora = DateTime.UtcNow; // Sempre salve em UTC!
        Status = StatusTransacao.Pendente;
        ChaveIdempotencia = chaveIdempotencia;
    }

    // Eventos
    public void MarcarComoConcluida() => Status = StatusTransacao.Concluida;
    public void MarcarComoFalha() => Status = StatusTransacao.Falha;
}
