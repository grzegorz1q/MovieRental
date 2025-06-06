import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Client } from '../../../../core/models/client.model';
import { ClientsApiService } from '../../../../core/services/api/clients-api.service';

@Component({
  selector: 'app-client-card',
  imports: [],
  templateUrl: './client-card.component.html',
  styleUrl: './client-card.component.scss'
})
export class ClientCardComponent {
  @Input() client!: Client;
  @Output() clientRemoved = new EventEmitter<void>();

  constructor(private clientService: ClientsApiService){}
  removeClient(){
    this.clientService.removeClient(this.client.id!).subscribe({
      next: (message) => {
        console.log(message);
        this.clientRemoved.emit();
      },
      error: (error) =>{
        console.error(error);
      }
    });
  }
}
