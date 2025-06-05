import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Rent } from '../../models/rent.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RentsApiService {
  private readonly apiUrl = `${environment.apiUrl}/rents`;
  constructor(private http: HttpClient) { }
  addRent(rent: Rent): Observable<Rent>{
    return this.http.post<Rent>(`${environment.apiUrl}/rents`, rent);
  }
}
