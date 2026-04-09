import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/app/models/usuario.model';
import { UsuariosService } from 'src/app/services/usuarios.service';
import { TransferenciaService } from 'src/app/services/transferencia.service';
import { TransferenciaRequest, TransferenciaResponse } from 'src/app/models/transferencia.model';
import { ToastrService } from 'ngx-toastr'; // 1. Importe o Toastr

@Component({
  selector: 'app-transferencia',
  templateUrl: './transferencia.component.html',
  styleUrls: ['./transferencia.component.css']
})
export class TransferenciaComponent implements OnInit {
  clientes: Usuario[] = [];
  filtradosOrigem: Usuario[] = [];
  filtradosDestino: Usuario[] = [];

  buscaOrigem: string = '';
  buscaDestino: string = '';
  showOrigem: boolean = false;
  showDestino: boolean = false;

  request: TransferenciaRequest = {
    usuarioRemetenteId: 0,
    usuarioDestinatarioId: 0,
    valor: 0,
    chaveIdempotencia: ''
  };

  carregando: boolean = false;
  comprovante: TransferenciaResponse | null = null;

  constructor(
    private usuariosService: UsuariosService,
    private transferenciaService: TransferenciaService,
    private toastr: ToastrService 
  ) {}

  ngOnInit(): void {
    this.carregarClientes();
    this.request.chaveIdempotencia = (crypto as any).randomUUID();
  }

  carregarClientes() {
    this.usuariosService.getUsuarios().subscribe({
      next: (res) => {
        this.clientes = res;
        this.filtradosOrigem = res;
        this.filtradosDestino = res;
      },
      error: (err) => {
        this.toastr.error('Não foi possível carregar a lista de usuários.', 'Erro de Conexão');
      }
    });
  }

  filtrarOrigem() {
    this.showOrigem = true;
    const termo = this.buscaOrigem.toLowerCase();
    this.filtradosOrigem = this.clientes.filter(u => 
      (u.nome.toLowerCase().includes(termo) || u.documento.includes(termo)) &&
      u.id !== this.request.usuarioDestinatarioId
    );
  }

  selecionarOrigem(user: Usuario) {
    this.request.usuarioRemetenteId = user.id;
    this.buscaOrigem = user.nome;
    this.showOrigem = false;
  }

  filtrarDestino() {
    this.showDestino = true;
    const termo = this.buscaDestino.toLowerCase();
    this.filtradosDestino = this.clientes.filter(u => 
      (u.nome.toLowerCase().includes(termo) || u.documento.includes(termo)) &&
      u.id !== this.request.usuarioRemetenteId
    );
  }

  selecionarDestino(user: Usuario) {
    this.request.usuarioDestinatarioId = user.id;
    this.buscaDestino = user.nome;
    this.showDestino = false;
  }

  confirmarEnvio() {
    // Validação com Toastr em vez de alert
    if (!this.request.usuarioRemetenteId || !this.request.usuarioDestinatarioId || this.request.valor <= 0) {
      this.toastr.warning('Preencha todos os campos e o valor antes de continuar.', 'Atenção');
      return;
    }

    const payload: TransferenciaRequest = {
      usuarioRemetenteId: Number(this.request.usuarioRemetenteId),
      usuarioDestinatarioId: Number(this.request.usuarioDestinatarioId),
      valor: Number(this.request.valor),
      chaveIdempotencia: this.request.chaveIdempotencia
    };

    if (confirm(`Confirmar transferência de R$ ${payload.valor}?`)) {
      this.carregando = true; // Ativa o Spinner
      
      this.transferenciaService.realizarTransferencia(payload).subscribe({
        next: (res) => {
          this.comprovante = res;
          this.carregando = false;
          this.toastr.success('Transferência realizada com sucesso!', 'NexusBank');
          
          this.request.chaveIdempotencia = (crypto as any).randomUUID();
        },
        error: (err) => {
          this.carregando = false; 
          console.error('Erro 400 Detalhado:', err.error);
          
          const erroMsg = err.error?.erro || 'Erro na transação. Verifique os dados.';
          this.toastr.error(erroMsg, 'Falha na Transferência');
        }
      });
    }
  }

  novaTransferencia() {
    this.comprovante = null;
    this.request.valor = 0;
    this.request.usuarioRemetenteId = 0;
    this.request.usuarioDestinatarioId = 0;
    this.buscaOrigem = '';
    this.buscaDestino = '';
    this.request.chaveIdempotencia = (crypto as any).randomUUID();
    this.toastr.info('Formulário resetado.', 'Sistema');
  }
}