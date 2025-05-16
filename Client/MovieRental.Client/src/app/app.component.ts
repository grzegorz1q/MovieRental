import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MoviesListComponent } from "./features/movies/pages/movies-list/movies-list.component";
import { MovieCardComponent } from "./features/movies/components/movie-card/movie-card.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'MovieRental.Client';
}
