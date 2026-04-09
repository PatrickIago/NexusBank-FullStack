import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Endereco } from '../models/usuario.model';

@Injectable({ providedIn: 'root' })
export class EnderecosService {
  
  // Forçando 5018 para garantir que os testes funcionem
  private apiUrl = 'http://localhost:5018/api/enderecos';

  constructor(private http: HttpClient) {}

  getEnderecos() {
    return this.http.get<Endereco[]>(this.apiUrl);
  }

  getEnderecoById(id: number) {
    return this.http.get<Endereco>(`${this.apiUrl}/${id}`);
  }

  criarEndereco(endereco: Endereco) {
    return this.http.post(this.apiUrl, endereco);
  }

  atualizarEndereco(id: number, endereco: Endereco) {
    return this.http.put(`${this.apiUrl}/${id}`, endereco);
  }

  deletarEndereco(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}