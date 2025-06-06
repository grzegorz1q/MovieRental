import { NgIf } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Client } from '../../../../core/models/client.model';
import { ClientsApiService } from '../../../../core/services/api/clients-api.service';

@Component({
  selector: 'app-add-client-modal',
  imports: [FormsModule, NgIf],
  templateUrl: './add-client-modal.component.html',
  styleUrl: './add-client-modal.component.scss'
})
export class AddClientModalComponent {
  newClient: Client = {
    firstName: '',
    lastName: '',
    email: '',
    address: '',
    phoneNumber: null!
  }
  isLoading: boolean = false;
  message: string = '';
  @Output() clientAdded = new EventEmitter<void>();
  @Output() closeModal = new EventEmitter<void>();
  constructor(private clientsService: ClientsApiService){}
  addClient(){
    this.isLoading = true;
    this.clientsService.addClient(this.newClient).subscribe({
      next: (response) =>{
        this.isLoading = false;
        this.message = '';
        this.clientAdded.emit();
      },
      error: (error) =>{
        this.message = '';
        this.message = error.error;
        this.isLoading = false;
      }
    })
  }
}
