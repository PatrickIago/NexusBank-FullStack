import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class UsuariosService {
  private apiUrl = 'http://localhost:5018/api/users';

  constructor(private http: HttpClient) {}

  getUsuarios() { return this.http.get<any[]>(this.apiUrl); }

  criarUsuario(usuario: any) { return this.http.post(this.apiUrl, usuario); }

  atualizarUsuario(id: number, usuario: any) {
    return this.http.put(`${this.apiUrl}/${id}`, usuario);
  }

  deletarUsuario(id: number) { return this.http.delete(`${this.apiUrl}/${id}`); }
}