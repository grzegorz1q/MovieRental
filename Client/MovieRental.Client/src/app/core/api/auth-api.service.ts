import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthApiService {
  private readonly apiUrl = `${environment.apiUrl}/account`;
  constructor(private http: HttpClient) { }
  login(email: string, password: string) {
    return this.http.post(`${this.apiUrl}/login`, {email, password}, {responseType: 'text'});
  }
}
