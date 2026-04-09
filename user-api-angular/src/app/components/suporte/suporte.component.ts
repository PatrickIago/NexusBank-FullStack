import { Component, OnInit } from '@angular/core';
import { SuporteService } from 'src/app/services/suporte.service';
import { ToastrService } from 'ngx-toastr';
import { TransferenciaResponse } from 'src/app/models/transferencia.model';
import { SaldoResponse } from 'src/app/models/suporte.model';

@Component({
  selector: 'app-suporte',
  templateUrl: './suporte.component.html',
  styleUrls: ['./suporte.component.css']
})
export class SuporteComponent implements OnInit {
  documentoBusca: string = '';
  cidadeBusca: string = '';

  transacoes: TransferenciaResponse[] = [];
  saldoResultado: SaldoResponse | null = null;
  
  carregando: boolean = false;

  constructor(
    private suporteService: SuporteService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
  }

  /**
   * Consulta o saldo do cliente pelo documento (CPF/CNPJ)
   */
  buscarSaldo() {
    if (!this.validarDocumento()) return;

    this.carregando = true;
    this.limparResultados();

    this.suporteService.obterSaldoPorDocumento(this.documentoBusca).subscribe({
      next: (res) => {
        this.saldoResultado = res;
        this.carregando = false;
        this.toastr.success('Saldo consultado com sucesso!');
      },
      error: (err) => {
        this.carregando = false;
        const msg = err.error?.message || 'Erro ao buscar saldo.';
        this.toastr.error(msg, 'Falha no Suporte');
      }
    });
  }

  /**
   * Consulta a lista de transações (Extrato) do cliente
   */
  buscarHistorico() {
    if (!this.validarDocumento()) return;

    this.carregando = true;
    this.limparResultados();

    this.suporteService.obterTransacoes(this.documentoBusca).subscribe({
      next: (res) => {
        this.transacoes = res;
        this.carregando = false;
        if (res.length === 0) {
          this.toastr.info('Nenhuma transação encontrada para este cliente.');
        } else {
          this.toastr.success(`${res.length} transações encontradas.`);
        }
      },
      error: (err) => {
        this.carregando = false;
        this.toastr.error('Erro ao carregar histórico de transações.');
      }
    });
  }

  /**
   * Gera e baixa o arquivo CSV de usuários por cidade/estado
   */
  exportarCsv() {
    if (!this.cidadeBusca) {
      this.toastr.warning('Digite uma cidade ou estado para exportar.');
      return;
    }

    this.suporteService.exportarUsuariosPorCidade(this.cidadeBusca).subscribe({
      next: (blob) => {
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = `usuarios_${this.cidadeBusca.toLowerCase()}.csv`;
        a.click();
        window.URL.revokeObjectURL(url);
        this.toastr.success('CSV gerado com sucesso!');
      },
      error: () => this.toastr.error('Erro ao gerar relatório CSV.')
    });
  }

  /**
   * Abre o comprovante da transação em PDF em uma nova aba
   */
  downloadPdf(transacaoId: string) {
    this.suporteService.baixarPdf(transacaoId).subscribe({
      next: (blob) => {
        const url = window.URL.createObjectURL(blob);
        window.open(url, '_blank');
        // Opcional: window.URL.revokeObjectURL(url); após um tempo
      },
      error: () => this.toastr.error('Erro ao gerar PDF do comprovante.')
    });
  }

  // Auxiliares
  private validarDocumento(): boolean {
    if (!this.documentoBusca || this.documentoBusca.length < 11) {
      this.toastr.warning('Informe um documento válido (CPF/CNPJ).');
      return false;
    }
    return true;
  }

  public limparResultados() {
    this.transacoes = [];
    this.saldoResultado = null;
  }
}