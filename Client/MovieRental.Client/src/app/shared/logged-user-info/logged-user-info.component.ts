import { Component } from '@angular/core';
import { AccountApiService } from '../../core/services/api/account-api.service';
import { Client } from '../../core/models/client.model';
import { Employee } from '../../core/models/employee.model';
import { AuthService } from '../../core/services/auth/auth.service';
import { NgIf, NgSwitch, NgSwitchCase, NgSwitchDefault } from '@angular/common';
import { NavbarComponent } from "../navbar/navbar.component";

@Component({
  selector: 'app-logged-user-info',
  imports: [NgIf, NavbarComponent],
  templateUrl: './logged-user-info.component.html',
  styleUrl: './logged-user-info.component.scss'
})
export class LoggedUserInfoComponent {
  client!: Client;
  employee!: Employee;
  role: string = '';
  ngOnInit() {
    this.role = this.authService.getRole();
    this.getLoggeduserInfo();
  }
  constructor(private readonly accountService: AccountApiService, private readonly authService: AuthService){}
  getLoggeduserInfo() {
    this.accountService.getInfoAboutLoggedUser().subscribe({
      next: (user) => {
        if (this.role === 'Client') {
        this.client = user as Client;
      } else if (this.role === 'Employee' || this.role === 'Admin') {
        this.employee = user as Employee;
      }
      },
      error: (error) => {
        console.error('Error fetching logged user info:', error);
      }
    });
  }
}
