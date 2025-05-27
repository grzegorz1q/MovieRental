import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Client } from '../../models/client.model';
import { Observable } from 'rxjs';
import { Employee } from '../../models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class AccountApiService {
  private readonly apiUrl = `${environment.apiUrl}/account`;
  constructor(private http: HttpClient) { }
  login(email: string, password: string) {
    return this.http.post(`${this.apiUrl}/login`, {email, password}, {responseType: 'text'});
  }
  register(client: Client){
    return this.http.post(`${this.apiUrl}/register`, client);
  }
  getInfoAboutLoggedUser(): Observable<Client | Employee>{
    return this.http.get<Client | Employee>(`${this.apiUrl}/me`);
  }
}
