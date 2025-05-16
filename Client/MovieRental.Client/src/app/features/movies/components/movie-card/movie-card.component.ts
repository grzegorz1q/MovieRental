import { Component, Input } from '@angular/core';
import { Movie } from '../../../../core/models/movie.model';

@Component({
  selector: 'app-movie-card',
  imports: [],
  templateUrl: './movie-card.component.html',
  styleUrl: './movie-card.component.scss'
})
export class MovieCardComponent {
  @Input() movie!: Movie;
}
