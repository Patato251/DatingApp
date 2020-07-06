import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';

// Array of routes for mutliple routes required
// Operates on first match basis, requiring wildcard last as it has to check through other options first
export const appRoutes: Routes = [
  { path: '', component: HomeComponent}, // Home route
  // Dummy route that allows protection of the children paths
  {
    path: '', // Route Ammendment 
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'members', component: MemberListComponent}, // Members route
      { path: 'messages', component: MessagesComponent}, // Messages route
      { path: 'lists', component: ListsComponent}, // Lists route
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full'} // Wildcard route to lead to homepage full url
];

