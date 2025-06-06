import { Component } from '@angular/core';
import { NavbarComponent } from '../../../../shared/navbar/navbar.component';
import { NgFor, NgIf } from '@angular/common';
import { ClientsApiService } from '../../../../core/services/api/clients-api.service';
import { Client } from '../../../../core/models/client.model';
import { AddClientModalComponent } from '../../components/add-client-modal/add-client-modal.component';
import { ClientCardComponent } from '../../components/client-card/client-card.component';

@Component({
  selector: 'app-clients-list',
  imports: [NavbarComponent, NgIf, NgFor, AddClientModalComponent, ClientCardComponent],
  templateUrl: './clients-list.component.html',
  styleUrl: './clients-list.component.scss'
})
export class ClientsListComponent {
  clients: Client[] = [];
  showAddClientModal = false;
  message: string = '';
  constructor(private clientsService: ClientsApiService){}
  ngOnInit(){
    this.getClients();
  }
  getClients(){
    this.clientsService.getClients().subscribe({
      next: (clients) =>{
        this.clients = clients;
      },
      error: (error) =>{
        console.error('Error fetching clients: ', error);
      }
    })
  }
}
