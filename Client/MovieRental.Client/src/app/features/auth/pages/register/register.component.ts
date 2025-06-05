import { Component } from '@angular/core';
import { AccountApiService } from '../../../../core/services/api/account-api.service';
import { Router, RouterLink } from '@angular/router';
import { Client } from '../../../../core/models/client.model';
import { FormsModule } from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { from } from 'rxjs';

@Component({
  selector: 'app-register',
  imports: [FormsModule, NgIf, NgFor, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  newClient: Client = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    confirmPassword: '',
    address: '',
    phoneNumber: null!
  };
  errorMessages: string[] = [];
  successMessage: string = '';
  isLoading: boolean = false;
  constructor(private readonly accountApiService: AccountApiService, private readonly router: Router) { }

  register() {
    this.isLoading = true;
    this.accountApiService.register(this.newClient).subscribe({
      next: (response) => {
        this.errorMessages = [];
        this.successMessage = 'Rejestracja zakończona sukcesem. Sprawdź swoją skrzynkę e-mail, aby aktywować konto.';
        this.isLoading = false;
      },
      error: (error) => {
        const errData = error.error;
        this.errorMessages = [];
        if (errData.errors && typeof errData.errors === 'object') {
          for (const key in errData.errors) {
            if (errData.errors.hasOwnProperty(key)) {
              const fieldErrors = errData.errors[key];
              this.errorMessages.push(...fieldErrors);
            }
          }
        } 
        else if (typeof errData === 'string') {
          this.errorMessages.push(errData);
        }
        this.isLoading = false;
      }
    });
  }
}
