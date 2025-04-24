import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path:"",
    loadComponent : () => import("@features/home/pages/home-page/home-page.component").then(c => c.HomePageComponent),
  },
  {
    path:"DrawResult",
    loadComponent : () => import("@features/draw-result/pages/draw-result-page/draw-result-page.component").then(c => c.DrawResultPageComponent),
  },
  {
    path:"BuyTicket",
    loadComponent : () => import("@features/buy-draw-ticket/pages/buy-draw-ticket-page/buy-draw-ticket-page.component").then(c => c.BuyDrawTicketPageComponent),
  },
  {
    path:"Dashboard",
    loadComponent : () => import("@features/dashboard/pages/dashboard-page/dashboard-page.component").then(c => c.DashboardPageComponent),
  },
  {
    path:"Profile",
    loadComponent : () => import("@features/profile/pages/profile-page/profile-page.component").then(c => c.ProfilePageComponent),
  },
  {
    path:'**', redirectTo:'/'
  }
];
