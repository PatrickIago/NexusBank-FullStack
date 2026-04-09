export interface Endereco {
  id?: number;
  rua: string;
  numero: string;
  cidade: string;
  estado: string;
  usuarioId: number;
}

export interface Usuario {
  id: number;
  nome: string;
  idade: number;
  email: string;
  celular: string;
  documento: string;
  tipoPessoa: string;
  endereco: Endereco;
  saldo: number;
}