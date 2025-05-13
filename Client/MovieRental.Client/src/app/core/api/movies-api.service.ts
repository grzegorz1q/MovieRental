import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movie } from './models/movie.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MoviesApiService {

  private readonly apiUrl = 'http://localhost:5178/movies';
  constructor(private http: HttpClient) { }
  
  getMovies(): Observable<Movie[]>{
    return this.http.get<Movie[]>(this.apiUrl);
  }
}
