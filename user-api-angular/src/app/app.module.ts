import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

// --- IMPORTAÇÕES PARA ANIMAÇÃO E TOASTR ---
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { EnderecosComponent } from './components/endereco/endereco.component';
import { TransferenciaComponent } from './components/transferencia/transferencia.component';
import { SuporteComponent } from './components/suporte/suporte.component';

@NgModule({
  declarations: [
    AppComponent,
    UsuariosComponent, 
    DashboardComponent, 
    EnderecosComponent, 
    TransferenciaComponent, SuporteComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    
    BrowserAnimationsModule, 
    ToastrModule.forRoot({
      timeOut: 4000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      progressBar: true
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }