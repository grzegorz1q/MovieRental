import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }
  getRole(): string {
    const token = localStorage.getItem('token');
    if (token) {
      const payload: any = jwtDecode(token);
      return payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    }
    return '';
  }
  getId(): string {
    const token = localStorage.getItem('token');
    if (token) {
      const payload: any = jwtDecode(token);
      return payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    }
    return '';
  }
}
function jwtDecode(token: string): any {
  const payload = token.split('.')[1];
  const decodedPayload = atob(payload);
  return JSON.parse(decodedPayload);
}
