import { Component } from '@angular/core';
import { Rent } from '../../../../core/models/rent.model';
import { ClientsApiService } from '../../../../core/services/api/clients-api.service';
import { Client } from '../../../../core/models/client.model';

@Component({
  selector: 'app-rents-dashboard',
  imports: [],
  templateUrl: './rents-dashboard.component.html',
  styleUrl: './rents-dashboard.component.scss'
})
export class RentsDashboardComponent {
  clients: Client[] = [];
  clientRents: Rent[] = [];

  constructor(private clientsService: ClientsApiService){}

  getClients(){
    this.clientsService.getClients().subscribe({
      next: (clients) =>{
        this.clients = clients;
      },
      error: (error) => {
        console.error('Error fetching clients: ', error);
      }
    })
  }
  getClientRents(id: number){
    this.clientsService.getClientRents(id).subscribe({
      next: (rents) =>{
        this.clientRents = rents;
      },
      error: (error) =>{
        console.error('Error fetching client rents: ', error);
      }
    })
  }
}
