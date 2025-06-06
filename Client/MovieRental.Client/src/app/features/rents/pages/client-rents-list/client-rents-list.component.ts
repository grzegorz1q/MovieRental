import { Component } from '@angular/core';
import { Rent } from '../../../../core/models/rent.model';
import { ClientsApiService } from '../../../../core/services/api/clients-api.service';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { NavbarComponent } from '../../../../shared/navbar/navbar.component';
import { DatePipe, NgFor, NgIf } from '@angular/common';
import { RentCardComponent } from '../../components/rent-card/rent-card.component';

@Component({
  selector: 'app-client-rents-list',
  imports: [NavbarComponent, NgIf, NgFor, RentCardComponent],
  templateUrl: './client-rents-list.component.html',
  styleUrl: './client-rents-list.component.scss'
})
export class ClientRentsListComponent {
  rents: Rent[] = [];
  constructor(private clientsService: ClientsApiService, private authService: AuthService){}
  ngOnInit(){
    this.getRents();
  }
  getRents(){
    const id = Number(this.authService.getId());
    this.clientsService.getClientRents(id).subscribe({
      next: (rents) =>{
        this.rents = rents;
      },
      error: (error) =>{
        console.error('Error fetching rents: ', error);
      }
    })
  }
}
