import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Employee } from '../../../../core/models/employee.model';
import { EmployeesApiService } from '../../../../core/services/api/employees-api.service';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-add-employee-modal',
  imports: [FormsModule, NgIf],
  templateUrl: './add-employee-modal.component.html',
  styleUrl: './add-employee-modal.component.scss'
})
export class AddEmployeeModalComponent {
  newEmployee: Employee = {
    firstName: '',
    lastName: '',
    email: '',
    role: ''
  };
  isLoading: boolean = false
  @Output() employeeAdded = new EventEmitter<void>();
  @Output() closeModal = new EventEmitter<void>();
  constructor(private employeeService: EmployeesApiService){}
  addEmployee(){
    this.isLoading = true;
    this.employeeService.addEmployee(this.newEmployee).subscribe({
      next: (response) =>{
        this.isLoading = false;
        this.employeeAdded.emit();
      },
      error: (error) =>{
        console.error('Error adding employee: ', error);
        this.isLoading = false;
      }
    })
  }
}
