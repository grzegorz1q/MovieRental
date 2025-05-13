import { Component } from '@angular/core';
import { MoviesApiService } from '../../../../core/api/movies-api.service';
import { Movie } from '../../../../core/models/movie.model';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-movies-list',
  imports: [NgFor],
  templateUrl: './movies-list.component.html',
  styleUrl: './movies-list.component.scss'
})
export class MoviesListComponent {
  title = "Lista filmów";
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
