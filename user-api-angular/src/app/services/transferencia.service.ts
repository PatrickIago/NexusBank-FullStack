import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TransferenciaRequest, TransferenciaResponse } from '../models/transferencia.model';

@Injectable({
  providedIn: 'root'
})
export class TransferenciaService {
  private readonly API = 'http://localhost:5018/api/transferencias'; 

  constructor(private http: HttpClient) {}

  realizarTransferencia(request: TransferenciaRequest): Observable<TransferenciaResponse> {
    return this.http.post<TransferenciaResponse>(this.API, request);
  }
}