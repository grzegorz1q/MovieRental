import { Component } from '@angular/core';
import { EmployeesApiService } from '../../../../core/services/api/employees-api.service';
import { Employee } from '../../../../core/models/employee.model';
import { NgFor, NgIf } from '@angular/common';
import { EmployeeCardComponent } from "../../components/employee-card/employee-card.component";
import { NavbarComponent } from "../../../../shared/navbar/navbar.component";
import { AddEmployeeModalComponent } from "../../components/add-employee-modal/add-employee-modal.component";

@Component({
  selector: 'app-employees-list',
  imports: [NgFor, EmployeeCardComponent, NgIf, NavbarComponent, NavbarComponent, AddEmployeeModalComponent],
  templateUrl: './employees-list.component.html',
  styleUrl: './employees-list.component.scss'
})
export class EmployeesListComponent {
  employees: Employee[] = [];
  showAddEmployeeModal = false;
  message: string = '';
  constructor(private employeesService: EmployeesApiService){}
  ngOnInit(){
    this.getEmployees();
  }
  getEmployees(){
    this.employeesService.getEmployees().subscribe(
      {
        next: (employees) => {
          this.employees = employees;
        },
        error: (error) => {
          console.error('Error fetching employees: ', error);
        }
      }
    )
  }
}
