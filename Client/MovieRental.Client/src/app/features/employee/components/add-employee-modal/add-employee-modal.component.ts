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
  message: string = '';
  isLoading: boolean = false
  @Output() employeeAdded = new EventEmitter<void>();
  @Output() closeModal = new EventEmitter<void>();
  constructor(private employeeService: EmployeesApiService){}
  addEmployee(){
    this.isLoading = true;
    this.employeeService.addEmployee(this.newEmployee).subscribe({
      next: (response) =>{
        this.isLoading = false;
        this.message = '';
        this.employeeAdded.emit();
      },
      error: (error) =>{
        this.message = '';
        this.message = error.error;
        this.isLoading = false;
      }
    })
  }
}
