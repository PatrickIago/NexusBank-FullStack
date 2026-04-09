import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { EnderecosComponent } from './components/endereco/endereco.component';
import { TransferenciaComponent } from './components/transferencia/transferencia.component';
import { SuporteComponent } from './components/suporte/suporte.component';

const routes: Routes = [
  // Padrão 
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },

  // Dashboard
  { path: 'dashboard', component: DashboardComponent },

  // Usuarios
  { path: 'usuarios', component: UsuariosComponent },

  // Endereços
  { path: 'enderecos', component: EnderecosComponent },

   // Transferencias
  { path: 'transferencias', component: TransferenciaComponent },

  // Suporte
  { path: 'suporte', component: SuporteComponent },

  // Retorna ao dashboard
  { path: '**', redirectTo: 'dashboard' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }