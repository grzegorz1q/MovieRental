import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Rent } from '../../models/rent.model';
import { Client } from '../../models/client.model';

@Injectable({
  providedIn: 'root'
})
export class ClientsApiService {
  private readonly apiUrl = `${environment.apiUrl}/clients`;
  constructor(private http: HttpClient) { }

  getClientRents(id: number): Observable<Rent[]>{
    return this.http.get<Rent[]>(`${this.apiUrl}/${id}/rents`);
  }
  getClients(): Observable<Client[]>{
    return this.http.get<Client[]>(this.apiUrl);
  }
  addClient(client: Client): Observable<Client>{
    return this.http.post<Client>(`${this.apiUrl}`, client);
  }
  removeClient(id: number): Observable<string>{
    return this.http.delete(`${this.apiUrl}/${id}`, {responseType: 'text'});
  }
}
