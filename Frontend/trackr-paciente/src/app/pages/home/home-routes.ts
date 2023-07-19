import { Route } from "@angular/router";
import { HomePage } from "./home.page";
import { DashboardPage } from "./dashboard/dashboard.page";

export default [
  {
    path: '',
    component: HomePage,
    children: [
      { path: 'dashboard', component: DashboardPage },
    ]
  },
] as Route[];
