import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/app/models/usuario.model';
import { UsuariosService } from 'src/app/services/usuarios.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  
  usuarios: Usuario[] = [];
  totalPJ: number = 0;
  totalPF: number = 0;

  constructor(private service: UsuariosService) {}

  ngOnInit(): void {
    this.carregarDados();
  }

  carregarDados(): void {
    this.service.getUsuarios().subscribe({
      next: (res) => {
        this.usuarios = res;
        
        // Filtros robustos baseados nos valores do seu banco/API
        this.totalPF = this.usuarios.filter(u => 
          u.tipoPessoa?.toUpperCase() === 'PESSOAFISICA'
        ).length;

        this.totalPJ = this.usuarios.filter(u => 
          u.tipoPessoa?.toUpperCase() === 'PESSOAJURIDICA'
        ).length;
      },
      error: (err) => {
        console.error('Erro ao carregar dados do Dashboard:', err);
      }
    });
  }

  // Formata o texto longo para a sigla PF ou PJ na tabela
  formatarTipo(tipo: string): string {
    if (!tipo) return '-';
    return tipo.toUpperCase() === 'PESSOAFISICA' ? 'PF' : 'PJ';
  }
}