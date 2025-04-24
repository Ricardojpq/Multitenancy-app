import { Component } from '@angular/core';
import {CardModule} from 'primeng/card';
import {NgClass} from '@angular/common';
import {DollarSign, Calendar, Users, ShoppingCart, LucideAngularModule} from 'lucide-angular'
import {ButtonModule} from 'primeng/button';

@Component({
  selector: 'app-buy-tickets',
  imports: [CardModule, NgClass, LucideAngularModule, ButtonModule],
  templateUrl: './buy-tickets.component.html',
  styleUrl: './buy-tickets.component.css'
})
export class BuyTicketsComponent {
  readonly dollarSignIcon = DollarSign;
  readonly calendarIcon = Calendar;
  readonly usersIcon = Users;
  readonly shoppingCartIcon = ShoppingCart;

  tickets:Array<any> = [
    {
      id: 1,
      title: "Sorteo Semanal",
      price: "$5",
      prize: "$2,000",
      date: "Cada Viernes",
      popular: false,
      color: "from-lottery-blue to-lottery-teal",
    },
    {
      id: 2,
      title: "Sorteo Especial",
      price: "$10",
      prize: "$5,000",
      date: "15 de Octubre",
      popular: true,
      color: "from-lottery-purple to-lottery-blue",
    },
    {
      id: 3,
      title: "Gran Sorteo",
      price: "$25",
      prize: "$25,000",
      date: "1 de Noviembre",
      popular: false,
      color: "from-lottery-red to-lottery-purple",
    },
  ];
}
