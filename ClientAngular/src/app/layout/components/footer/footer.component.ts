import {Component} from '@angular/core';
import {LucideAngularModule, Award} from 'lucide-angular'
import {RouterModule} from '@angular/router';

@Component({
  selector: 'app-footer',
  imports: [LucideAngularModule,RouterModule],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css'
})
export class FooterComponent {
  readonly awardIcon = Award;
  year = new Date().getFullYear();
}
