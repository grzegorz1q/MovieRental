import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GeminiMovie } from '../../models/geminiMovie.model';

@Injectable({
  providedIn: 'root'
})
export class GeminiApiService {
  private readonly apiUrl = `${environment.apiUrl}/gemini/popular-movies`;
  constructor(private http: HttpClient) { }

  getGeminiMovies(): Observable<GeminiMovie[]>{
    return this.http.get<GeminiMovie[]>(this.apiUrl);
  }
}
