import { Component } from '@angular/core';
import {ButtonModule} from 'primeng/button';
import { MenubarModule } from 'primeng/menubar';
import {RouterModule} from '@angular/router';
import {LucideAngularModule, Award, User, Menu, X} from 'lucide-angular'
import {MenuItem} from 'primeng/api';

@Component({
  selector: 'app-nav-bar',
  imports: [RouterModule, ButtonModule, MenubarModule,LucideAngularModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {
  readonly awardIcon = Award;
  readonly menuIcon = Menu;
  readonly xIcon = X;
  readonly  userIcon = User;


  navItems:MenuItem[] = [
    { label: 'Inicio', url: '/' ,icon:'pi pi-home'},
    { label: 'Resultados', url: '/results' ,icon:'pi pi-trophy'},
    { label: 'Comprar Boletos', url: '/games' ,icon:'pi pi-shopping-cart'},
  ];
}
