import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { NgIf } from '@angular/common';
import { AuthService } from '../../core/services/auth/auth.service';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, NgIf],
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
  }
  checkLoginStatus() {
    const token = localStorage.getItem('token');
    this.isLoggedIn = !!token; // true jeśli token istnieje, false jeśli nie
  }
  logout() {
    localStorage.removeItem('token');
    this.isLoggedIn = false; 
    this.router.navigateByUrl('/movies');
  }
  login() {
    this.router.navigateByUrl('/login');
  }
  getRole(): string {
    return this.authService.getRole();
  }

}
