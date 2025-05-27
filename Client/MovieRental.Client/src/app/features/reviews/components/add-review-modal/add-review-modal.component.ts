import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Review } from '../../../../core/models/review.model';
import { MoviesApiService } from '../../../../core/services/api/movies-api.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-add-review-modal',
  imports: [FormsModule],
  templateUrl: './add-review-modal.component.html',
  styleUrl: './add-review-modal.component.scss'
})
export class AddReviewModalComponent {
  newReview: Review = {
    rating: null!,
    comment: ''
  };
  @Input() movieId!: number;
  @Output() reviewAdded = new EventEmitter<void>();
  @Output() closeModal = new EventEmitter<void>();
  constructor(private movieService: MoviesApiService, private route: ActivatedRoute) {}
  addReview() {
    this.movieService.addReview(this.movieId, this.newReview).subscribe({
      next: (response) => {
        this.newReview = { rating: null!, comment: '' };
        this.reviewAdded.emit();
      },
      error: (error) => {
        console.error('Error adding review:', error);
      }
    });
  }
}
