import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { HistoryComponent } from './pages/history/history.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'history', component: HistoryComponent }
];
