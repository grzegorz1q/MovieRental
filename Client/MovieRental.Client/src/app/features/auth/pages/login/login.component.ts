import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountApiService } from '../../../../core/services/api/account-api.service';
import { Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { NgIf } from '@angular/common';



@Component({
  selector: 'app-login',
  imports: [FormsModule, NgIf],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginObj: any = {
    email: '',
    password: ''
  };
  errorMessage: string = '';
  constructor(private readonly accountApiService: AccountApiService, private readonly authService: AuthService, private readonly router: Router) { }
  login() {
    const { email, password } = this.loginObj;
    this.accountApiService.login(email, password).subscribe({
      next: (response) => {
        localStorage.setItem('token', response);
        const role = this.authService.getRole();
        this.router.navigateByUrl('/movies');
        },
      error: (error) => {
        this.errorMessage = error.error;
      }
    });
  }
}
