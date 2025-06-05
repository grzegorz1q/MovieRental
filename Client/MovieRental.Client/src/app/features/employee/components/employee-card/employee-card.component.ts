import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Employee } from '../../../../core/models/employee.model';
import { NgClass, NgIf } from '@angular/common';
import { EmployeesApiService } from '../../../../core/services/api/employees-api.service';

@Component({
  selector: 'app-employee-card',
  imports: [NgClass],
  templateUrl: './employee-card.component.html',
  styleUrl: './employee-card.component.scss'
})
export class EmployeeCardComponent {
  @Input() employee!: Employee;
  @Output() employeeRemoved = new EventEmitter<void>();

  constructor(private employeesService: EmployeesApiService){}
  changeRole(){
  const newRole = this.employee.role === 'Admin' ? 1 : 0;
    this.employeesService.changeRole(this.employee.id!, newRole).subscribe({
      next: (message) => {
        this.employee.role = newRole === 0 ? 'Admin' : 'Employee';
      },
      error: (error) =>{
        console.error('Error while updating role: ', error);
      }
    });
  }
  removeEmployee(){
    this.employeesService.removeEmployee(this.employee.id!).subscribe({
      next: (message) => {
        console.log(message);
        this.employeeRemoved.emit();
      },
      error: (error) =>{
        console.error(error);
      }
    });
  }
}
