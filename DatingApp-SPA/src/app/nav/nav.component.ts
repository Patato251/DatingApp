import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  // Creates an empty object to be used in transfer of data from the login process
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    // Receive the Observable from login by calling the login service method
    // Store the result from the login method into the local model var
    this.authService.login(this.model).subscribe(next => {
      console.log('The user has logged in Successfully');
    }, error => {
      console.log('The user has failed to log in');
    });
  }

  // Log in method to check status of user signed in
  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  // Removes token from local storage to log out
  logOut () {
    localStorage.removeItem('token');
    console.log('Logged User Out');
  }
}
