export interface SaldoResponse {
  mensagem: string;
  saldo: number;
}

export interface ComprovanteSuporte {
  mensagem: string;
  transacaoId: string;
  dataHora: Date;
  remetente: string;
  documentoRemetente: string;
  destinatario: string;
  valor: number;
}