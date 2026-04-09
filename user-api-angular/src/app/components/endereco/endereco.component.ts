import { Component } from '@angular/core';
import { Endereco } from 'src/app/models/usuario.model';
import { EnderecosService } from 'src/app/services/enderecos.service';

@Component({
  selector: 'app-enderecos',
  templateUrl: './endereco.component.html',
  styleUrls: ['./endereco.component.css']
})
export class EnderecosComponent {
  idBusca: number | null = null;
  enderecoSelecionado: Endereco | null = null;
  exibirForm: boolean = false;

  constructor(private service: EnderecosService) {}

  buscarPorId() {
    if (!this.idBusca) return;

    this.service.getEnderecoById(this.idBusca).subscribe({
      next: (res) => {
        this.enderecoSelecionado = res;
        this.exibirForm = true;
      },
      error: () => {
        alert('Endereço não encontrado na porta 5018!');
        this.fecharTela();
      }
    });
  }

  novoEndereco() {
    this.idBusca = null;
    this.enderecoSelecionado = { rua: '', numero: '', cidade: '', estado: '', usuarioId: 0 };
    this.exibirForm = true;
  }

  salvar() {
    if (!this.enderecoSelecionado) return;

    if (this.enderecoSelecionado.id) {
      this.service.atualizarEndereco(this.enderecoSelecionado.id, this.enderecoSelecionado).subscribe({
        next: () => {
          alert('Atualizado com sucesso!');
          this.fecharTela();
        },
        error: () => alert('Erro ao atualizar.')
      });
    } else {
      this.service.criarEndereco(this.enderecoSelecionado).subscribe({
        next: () => {
          alert('Criado com sucesso!');
          this.fecharTela();
        },
        error: () => alert('Erro ao criar.')
      });
    }
  }

  deletar(id: number) {
    if (confirm(`Tem certeza que deseja excluir o endereço ID ${id}?`)) {
      this.service.deletarEndereco(id).subscribe({
        next: () => {
          alert('Excluído com sucesso!');
          this.fecharTela();
        },
        error: () => alert('Erro ao deletar.')
      });
    }
  }

  fecharTela() {
    this.exibirForm = false;
    this.enderecoSelecionado = null;
    this.idBusca = null;
  }
}