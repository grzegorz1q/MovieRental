import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MoviesListComponent } from "./features/movies/components/movies-list/movies-list.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MoviesListComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'MovieRental.Client';
}
