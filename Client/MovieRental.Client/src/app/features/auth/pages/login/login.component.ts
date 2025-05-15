import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthApiService } from '../../../../core/api/auth-api.service';



@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginObj: any = {
    email: '',
    password: ''
  };
  constructor(private readonly authApiService: AuthApiService) { }
  login() {
    const { email, password } = this.loginObj;
    this.authApiService.login(email, password).subscribe({
      next: (response) => {
        console.log('Login successful:', response);
        // Handle successful login, e.g., store token, redirect, etc.
      },
      error: (error) => {
        console.log('Login failed:', error);
        // Handle login error, e.g., show error message
      }
    });
  }
}
