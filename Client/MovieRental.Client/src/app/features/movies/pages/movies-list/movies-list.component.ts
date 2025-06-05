import { Component } from '@angular/core';
import { MoviesApiService } from '../../../../core/services/api/movies-api.service';
import { Movie } from '../../../../core/models/movie.model';
import { NgFor } from '@angular/common';
import { MovieCardComponent } from "../../components/movie-card/movie-card.component";
import { NavbarComponent } from "../../../../shared/navbar/navbar.component";

@Component({
  selector: 'app-movies-list',
  imports: [NgFor, MovieCardComponent, NavbarComponent],
  templateUrl: './movies-list.component.html',
  styleUrl: './movies-list.component.scss'
})
export class MoviesListComponent{
  title = "Lista filmów";
  
  movies: Movie[] = [];
  constructor(private moviesApiService: MoviesApiService) { }
  ngOnInit() {
    this.getMovies();
  }
  getMovies() {
    this.moviesApiService.getMovies().subscribe(
      {
        next: (movies) => {
          this.movies = movies;
        },
        error: (error) => {
          console.error('Error fetching movies:', error);
        }
      }
    );
  }
}
