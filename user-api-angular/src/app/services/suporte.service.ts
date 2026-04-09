import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SaldoResponse, ComprovanteSuporte } from '../models/suporte.model';
import { TransferenciaResponse } from '../models/transferencia.model';

@Injectable({
  providedIn: 'root'
})
export class SuporteService {
  private readonly API = 'http://localhost:5018/api/suporte';

  constructor(private http: HttpClient) {}

  /**
   * Obtém o histórico de transações filtrado por documento
   */
  obterTransacoes(documento: string): Observable<TransferenciaResponse[]> {
    return this.http.get<TransferenciaResponse[]>(`${this.API}/transacoes?documento=${documento}`);
  }

  /**
   * Consulta o saldo detalhado do cliente
   */
  obterSaldoPorDocumento(documento: string): Observable<SaldoResponse> {
    // Atenção: use o nome exato da rota que está no seu Controller C#
    return this.http.get<SaldoResponse>(`${this.API}/Obter-saldo-por-documento?documento=${documento}`);
  }

  /**
   * Baixa a lista de usuários de um determinado endereço em formato CSV
   */
  exportarUsuariosPorCidade(cidade: string): Observable<Blob> {
    return this.http.get(`${this.API}/exportar-usuarios-por-cidade?endereco=${cidade}`, {
      responseType: 'blob'
    });
  }

  /**
   * Baixa o PDF do comprovante de uma transação específica
   */
  baixarPdf(transacaoId: string): Observable<Blob> {
    return this.http.get(`${this.API}/comprovante-pdf?transacaoId=${transacaoId}`, {
      responseType: 'blob'
    });
  }
}