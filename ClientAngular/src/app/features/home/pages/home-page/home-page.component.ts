import { Component } from '@angular/core';
import {HeroComponent} from '@features/home/components/hero/hero.component';
import {BuyTicketsComponent} from '@features/home/components/buy-tickets/buy-tickets.component';
import {DrawResultsComponent} from '@features/home/components/draw-results/draw-results.component';
import {TestimonialsComponent} from '@features/home/components/testimonials/testimonials.component';
import {StatsComponent} from '@features/home/components/stats/stats.component';


@Component({
  selector: 'app-home-page',
  imports: [
    HeroComponent,
    BuyTicketsComponent,
    DrawResultsComponent,
    TestimonialsComponent,
    StatsComponent
  ],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent {

}
