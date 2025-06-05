import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountApiService } from '../../core/services/api/account-api.service';
import { NgIf } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  imports: [FormsModule, NgIf, RouterLink],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.scss'
})
export class ForgotPasswordComponent {
  email: string = '';
  successMessage: string = '';
  errorMessage: string = '';
  isLoading: boolean = false;
  constructor(private accountService: AccountApiService){}

  forgotPassword(){
    this.isLoading = true;
    this.accountService.forgotPassword(this.email).subscribe({
      next: (response) =>{
        this.errorMessage = '';
        this.successMessage = '';
        this.successMessage = 'Na podany adres email wysłano hasło tymczasowe';
        this.isLoading = false;
      },
      error: (error) =>{
        this.successMessage = '';
        this.errorMessage = '';
        this.errorMessage = error.error;
        this.isLoading = false;
      }
    })
  }
}
