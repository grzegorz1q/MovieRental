import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Movie } from './core/api/models/movie.model';
import { MoviesApiService } from './core/api/movies-api.service';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'MovieRental.Client';
  movies: Movie[] = [];
  constructor(private moviesApiService: MoviesApiService) { }
  ngOnInit() {
    this.getMovies();
  }
  getMovies() {
    this.moviesApiService.getMovies().subscribe(
      movies => {
        this.movies = movies;
      },
      error => {
        console.error('Error fetching movies:', error);
      }
    );
  }
}
