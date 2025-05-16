import { Component } from '@angular/core';
import { LoginComponent } from "../../features/auth/pages/login/login.component";
import { Router, RouterLink } from '@angular/router';
import { NgIf } from '@angular/common';
import { AuthService } from '../../core/services/auth/auth.service';

@Component({
  selector: 'app-navbar',
  imports: [LoginComponent, RouterLink, NgIf],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  isLoggedIn: boolean = false;
  role: string = '';
  constructor(private readonly authService: AuthService, private readonly router: Router) { }
  ngOnInit() {
    this.checkLoginStatus();
    this.role = this.getRole();
    console.log(this.role);
  }
  checkLoginStatus() {
    const token = localStorage.getItem('token');
    this.isLoggedIn = !!token; // Set isLoggedIn to true if token exists
  }
  logout() {
    localStorage.removeItem('token');
    this.isLoggedIn = false; 
  }
  login() {
    this.router.navigateByUrl('/login');
  }
  getRole(): string {
    return this.authService.getRole();
  }

}
