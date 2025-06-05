import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Employee } from '../../models/employee.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeesApiService {
  private readonly apiUrl = `${environment.apiUrl}/employees`;
  constructor(private http: HttpClient) { }

  getEmployees(): Observable<Employee[]>{
    return this.http.get<Employee[]>(this.apiUrl);
  }
  getEmployeeById(id: number): Observable<Employee>{
    return this.http.get<Employee>(`${this.apiUrl}/${id}`);
  }
  changeRole(id: number, role: number): Observable<string>{
    return this.http.patch(`${this.apiUrl}/roles/${id}`, {role}, {responseType: 'text'});
  }
  removeEmployee(id: number): Observable<string>{
    return this.http.delete(`${this.apiUrl}/${id}`, {responseType: 'text'});
  }
  addEmployee(employee: Employee): Observable<Employee>{
    const roleValue = employee.role === 'Admin'? 0 : 1;
    const employeeToSend = {
      ...employee,
      role: roleValue
    };
    return this.http.post<Employee>(`${this.apiUrl}`, employeeToSend);
  }
}
