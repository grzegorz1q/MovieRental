import { Component } from '@angular/core';
import { AccountApiService } from '../../../../core/services/api/account-api.service';
import { Router, RouterLink } from '@angular/router';
import { CreateClient } from '../../../../core/models/createClient.model';
import { FormsModule } from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-register',
  imports: [FormsModule, NgIf, NgFor, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  newClient: CreateClient = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    confirmPassword: '',
    address: '',
    phoneNumber: undefined
  };
  errorMessages: string[] = [];
  successMessage: string = '';
  constructor(private readonly accountApiService: AccountApiService, private readonly router: Router) { }

  register() {
    this.accountApiService.register(this.newClient).subscribe({
      next: (response) => {
        this.errorMessages = [];
        console.log('Registration successful:', response); //Dodać jakiś wyświetlający się na ekranie komunikat
        this.successMessage = 'Rejestracja zakończona sukcesem. Sprawdź swoją skrzynkę e-mail, aby aktywować konto.';
      },
      error: (error) => {
        console.log('Wiadomosc');
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
      }
    });
  }
}
