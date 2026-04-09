import { Component, OnInit } from '@angular/core';
import { UsuariosService } from 'src/app/services/usuarios.service';

@Component({
  selector: 'app-users',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {
  usuarios: any[] = [];
  usuariosFiltrados: any[] = [];
  exibirForm = false;
  filtroDocumento: string = '';
  usuarioSelecionado: any = this.limparObjeto();

  constructor(private service: UsuariosService) {}

  ngOnInit() { 
    this.carregarUsuarios(); 
  }

  carregarUsuarios() {
    this.service.getUsuarios().subscribe({
      next: (res) => {
        this.usuarios = res;
        this.usuariosFiltrados = res;
      },
      error: (err) => console.error('Erro ao carregar lista:', err)
    });
  }

  filtrarPorDocumento() {
    const termo = this.filtroDocumento.trim();
    this.usuariosFiltrados = !termo ? this.usuarios : 
      this.usuarios.filter(u => u.documento?.includes(termo));
  }

  novoUsuario() {
    this.usuarioSelecionado = this.limparObjeto();
    this.exibirForm = true;
  }

  editar(user: any) {
    // Clonamos o objeto para não alterar a tabela antes de salvar
    this.usuarioSelecionado = { 
      ...user, 
      endereco: user.endereco ? { ...user.endereco } : { rua: '', numero: '', cidade: '', estado: '' } 
    };
    this.exibirForm = true;
  }

  salvar() {
    // 1. Forçamos o ID a ser um número inteiro puro
    const idLimpo = parseInt(this.usuarioSelecionado.id);
    
    // 2. Limpeza de strings (removendo máscaras)
    const docLimpo = String(this.usuarioSelecionado.documento).replace(/\D/g, '');
    const celLimpo = String(this.usuarioSelecionado.celular).replace(/\D/g, '');

    if (this.usuarioSelecionado.id) {
      // --- LÓGICA PUT (IGUAL AO SEU POSTMAN) ---
      const { endereco, ...dados } = this.usuarioSelecionado;
      
      const payload = { 
        ...dados, 
        id: idLimpo,
        documento: docLimpo, 
        celular: celLimpo,
        // Mantemos como string se o Postman aceitou assim
        tipoPessoa: this.usuarioSelecionado.tipoPessoa 
      };

      console.log('Enviando para o servidor:', payload);

      this.service.atualizarUsuario(idLimpo, payload).subscribe({
        next: () => {
          console.log('Atualizado com sucesso!');
          this.finalizarAcao();
        },
        error: (err) => {
          console.error('Erro na API:', err);
          // Se o erro for 400, mostramos a mensagem de validação do C#
          const msg = err.error?.message || 'Verifique o CPF e a conexão.';
          alert('Erro ao atualizar: ' + msg);
        }
      });
    } else {
      // --- LÓGICA POST (CADASTRO) ---
      const payload = { 
        ...this.usuarioSelecionado, 
        documento: docLimpo, 
        celular: celLimpo
      };

      this.service.criarUsuario(payload).subscribe({
        next: () => this.finalizarAcao(),
        error: (err) => alert('Erro ao criar usuário.')
      });
    }
  }

  deletar(id: number) {
    if (confirm('Deseja excluir permanentemente?')) {
      this.service.deletarUsuario(id).subscribe(() => this.carregarUsuarios());
    }
  }

  limparObjeto() {
    return { 
      nome: '', 
      email: '', 
      celular: '', 
      documento: '', 
      idade: null, 
      tipoPessoa: 'PessoaFisica', // Padrão string
      endereco: { rua: '', numero: '', cidade: '', estado: '' } 
    };
  }

  private finalizarAcao() {
    this.exibirForm = false;
    this.carregarUsuarios();
  }
}