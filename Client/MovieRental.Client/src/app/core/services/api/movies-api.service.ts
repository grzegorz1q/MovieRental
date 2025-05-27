import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Movie } from '../../models/movie.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Review } from '../../models/review.model';

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
  getMovieById(id: number): Observable<Movie>{
    return this.http.get<Movie>(`${this.apiUrl}/${id}`).pipe(
      map(movie => ({
        ...movie,
        image: `${environment.apiUrl}/${movie.image}`
      }))
    );
  }
  getMovieReviews(id: number): Observable<Review[]>{
    return this.http.get<Review[]>(`${this.apiUrl}/${id}/reviews`);
  }
  addReview(movieId: number, review: Review): Observable<Review> {
    return this.http.post<Review>(`${this.apiUrl}/${movieId}/reviews`, review);
  }
}