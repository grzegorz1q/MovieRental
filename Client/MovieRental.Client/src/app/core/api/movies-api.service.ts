import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Movie } from '../models/movie.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MoviesApiService {

  private readonly apiUrl = `${environment.apiUrl}/movies`;
  constructor(private http: HttpClient) { }
  
  getMovies(): Observable<Movie[]>{
    return this.http.get<Movie[]>(this.apiUrl).pipe(
      map(movies =>{
        return movies.map(movie => ({
          ...movie,
          image: `${environment.apiUrl}/${movie.image}`
        }))
      })
    );
  }
}
