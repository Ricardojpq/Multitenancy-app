import { Component } from '@angular/core';
import {ButtonModule} from 'primeng/button';
import {RouterModule} from '@angular/router';
import {LucideAngularModule, ShoppingCart} from 'lucide-angular';

@Component({
  selector: 'app-hero',
  imports: [ButtonModule, RouterModule, LucideAngularModule],
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.css'
})
export class HeroComponent {
  readonly shoppingCartIcon = ShoppingCart;
}
