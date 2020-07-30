import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';

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
      { path: 'members', component: MemberListComponent, resolve: {users: MemberListResolver}}, // Members route
      { path: 'members/:id', component: MemberDetailComponent, resolve: {user: MemberDetailResolver}}, // Members route
      { path: 'member/edit', component: MemberEditComponent, resolve: {user: MemberEditResolver}}, // Member Edit Route
      { path: 'messages', component: MessagesComponent}, // Messages route
      { path: 'lists', component: ListsComponent}, // Lists route
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full'} // Wildcard route to lead to homepage full url
];

