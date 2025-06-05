import { Component } from '@angular/core';
import { Rent } from '../../../../core/models/rent.model';
import { ClientsApiService } from '../../../../core/services/api/clients-api.service';
import { Client } from '../../../../core/models/client.model';
import { DatePipe, NgFor, NgIf } from '@angular/common';
import { NavbarComponent } from "../../../../shared/navbar/navbar.component";
import { RentsApiService } from '../../../../core/services/api/rents-api.service';
import { Movie } from '../../../../core/models/movie.model';
import { MoviesApiService } from '../../../../core/services/api/movies-api.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-rents-dashboard',
  imports: [NgFor, NgIf, NavbarComponent, FormsModule, DatePipe],
  templateUrl: './rents-dashboard.component.html',
  styleUrl: './rents-dashboard.component.scss'
})
export class RentsDashboardComponent {
  newRent: Rent = {
    movieId: null!,
    clientId: null!
  }
  clients: Client[] = [];
  movies: Movie[] = [];
  clientRents: Rent[] = [];
  expandedClientId: number | null = null;
  successMessage: string = '';
  errorMessage: string = '';
  constructor(private clientsService: ClientsApiService, private rentsService: RentsApiService, private moviesService: MoviesApiService){}
  ngOnInit(){
    this.getClients();
    this.getMovies();
  }
  getMovies() {
    this.moviesService.getMovies().subscribe({
      next: (movies) => {
        this.movies = movies;
      },
      error: (error) => {
        console.error('Error fetching movies: ', error);
      }
    });
  }
  addRent(){
    this.rentsService.addRent(this.newRent).subscribe({
      next: (response) =>{
        this.errorMessage = '';
        this.successMessage = 'Dodano wypożyczenie';
      },
      error: (error) =>{
        this.successMessage = '';
        this.errorMessage = 'Wybrany klient posiada już ten film'
      }
    })
  }
  returnMovie(rentId: number) {
  this.rentsService.returnMovie(rentId).subscribe({
    next: () => {
      if (this.expandedClientId) {
        this.getClientRents(this.expandedClientId);
      }
    },
    error: (error) => {
      console.error('Error while returning movie', error);
    }
  });
}

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


  //Pomocnicze
  toggleClientRents(clientId: number) {
    if (this.expandedClientId === clientId) {
      this.expandedClientId = null;
      this.clientRents = [];
    } else {
      this.expandedClientId = clientId;
      this.getClientRents(clientId);
    }
  }
}
