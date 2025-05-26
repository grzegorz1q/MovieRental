import { Component } from '@angular/core';
import { Movie } from '../../../../core/models/movie.model';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MoviesApiService } from '../../../../core/services/api/movies-api.service';
import { NgFor, NgIf } from '@angular/common';
import { Review } from '../../../../core/models/review.model';
import { FormsModule } from '@angular/forms';
import { AddReviewModalComponent } from '../../../reviews/components/add-review-modal/add-review-modal.component';

@Component({
  selector: 'app-movie-details',
  imports: [NgIf, NgFor, FormsModule, AddReviewModalComponent],
  templateUrl: './movie-details.component.html',
  styleUrl: './movie-details.component.scss'
})
export class MovieDetailsComponent {
  movieId!: number;
  movie!: Movie;
  reviews: Review[] = [];
  newReview!: Review;
  showReviewModal: boolean = false;
  constructor(private route: ActivatedRoute, private movieService: MoviesApiService) { }
  ngOnInit() {
    this.route.params.subscribe(params => {
      this.movieId = +params['id'];
      this.getMovieById(this.movieId);
      this.getMovieReviews(this.movieId);
    });
  }
  getMovieById(id: number) {
    this.movieService.getMovieById(id).subscribe({
      next: (movie) => {
        this.movie = movie;
      },
      error: (error) => {
        console.error('Error fetching movie details:', error);
      }
    });
  }
  getMovieReviews(id: number) {
    this.movieService.getMovieReviews(id).subscribe({
      next: (reviews) => {
        this.reviews = reviews;
      },
      error: (error) => {
        console.error('Error fetching movie reviews:', error);
      }
    });
  }
}
