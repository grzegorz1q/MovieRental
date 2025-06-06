import { Component, Input } from '@angular/core';
import { Rent } from '../../../../core/models/rent.model';
import { RentsApiService } from '../../../../core/services/api/rents-api.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-rent-card',
  imports: [DatePipe],
  templateUrl: './rent-card.component.html',
  styleUrl: './rent-card.component.scss'
})
export class RentCardComponent {
  @Input() rent!: Rent;
  constructor(){}
}
