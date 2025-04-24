import {Component} from '@angular/core';
import {Card} from 'primeng/card';
import {Award, Calendar,Search, LucideAngularModule} from 'lucide-angular'
import {ButtonDirective} from 'primeng/button';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-draw-results',
  imports: [
    Card,
    LucideAngularModule,
    ButtonDirective,
    RouterLink
  ],
  templateUrl: './draw-results.component.html',
  styleUrl: './draw-results.component.css'
})
export class DrawResultsComponent {
  readonly calendarIcon = Calendar;
  readonly awardIcon = Award;
  readonly searchIcon = Search;
  // border bg-card text-card-foreground shadow-sm ticket-card
  displayCount: number = 2; // Number of results to display
  pastResults: Array<any> = [
    {
      id: 1,
      title: "Sorteo Semanal",
      date: "8 de Octubre, 2023",
      prize: "$2,000",
      winningNumbers: [7, 12, 25, 32, 41],
      winner: "Juan Pérez",
    },
    {
      id: 2,
      title: "Sorteo Especial",
      date: "1 de Octubre, 2023",
      prize: "$5,000",
      winningNumbers: [3, 19, 27, 36, 45],
      winner: "María Gómez",
    },
    {
      id: 3,
      title: "Gran Sorteo",
      date: "15 de Septiembre, 2023",
      prize: "$25,000",
      winningNumbers: [5, 11, 18, 29, 44],
      winner: "Carlos Rodríguez",
    },
  ];

  showMore(){
    this.displayCount = this.pastResults.length;
  }

}
