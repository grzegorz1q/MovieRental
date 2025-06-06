import { Component } from '@angular/core';
import { GeminiMovie } from '../../../../core/models/geminiMovie.model';
import { GeminiApiService } from '../../../../core/services/api/gemini-api.service';
import { errorContext } from 'rxjs/internal/util/errorContext';
import { NgClass, NgFor, NgIf } from '@angular/common';
import { NavbarComponent } from '../../../../shared/navbar/navbar.component';

@Component({
  selector: 'app-gemini-dashboard',
  imports: [NgClass, NgIf, NgFor, NavbarComponent],
  templateUrl: './gemini-dashboard.component.html',
  styleUrl: './gemini-dashboard.component.scss'
})
export class GeminiDashboardComponent {
  geminiMovies: GeminiMovie[] = [];
  isLoading: boolean = false;
  constructor(private geminiService: GeminiApiService){}

  ngOnInit(){
    this.getGeminiMovies();
  }

  getGeminiMovies(){
    this.isLoading = true;
    this.geminiService.getGeminiMovies().subscribe({
      next: (movies) =>{
        this.geminiMovies = movies;
        this.isLoading = false;
      },
      error: (error) =>{
        console.error('Error fetching gemini movies: ', error);
        this.isLoading = false;
      }
    })
  }
}
