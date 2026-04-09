export interface TransferenciaRequest {
  usuarioRemetenteId: number;
  usuarioDestinatarioId: number;
  valor: number;
  chaveIdempotencia: string; 
}

export interface TransferenciaResponse {
  transacaoId: string;   
  valor: number;        
  destinatario: string; 
  status: string;       
  dataHora: Date;       
}