import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  // Creates an empty object to be used in transfer of data from the login process
  model: any = {};

  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    // Receive the Observable from login by calling the login service method
    // Store the result from the login method into the local model var
    this.authService.login(this.model).subscribe(next => { // Next Response
      this.alertify.success('The user has logged in Successfully');
    }, error => { // Error response
      this.alertify.error(error);
    }, () => { // Completed Response (can throw this into next method)
      this.router.navigate(['/members']);
    });
  }

  // Log in method to check status of user signed in
  loggedIn() {
    return this.authService.loggedIn(); // replace original method with authService method for injection
  }

  // Removes token from local storage to log out
  logOut() {
    localStorage.removeItem('token');
    this.alertify.message('Logged User Out');
    this.router.navigate(['/home']);
  }
}
