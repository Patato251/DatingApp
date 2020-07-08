import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/'; // http://localhost:5000/api/
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) { }

  // Login Method with input type model to transmit and receive data for (from database and transmit token)
  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model) // replicating postman behaviour, expects to receive json
      .pipe( // Chain rxjs operators to our requests, similar to js functionality with observables
        map((response: any) => {
          const user = response;
          if (user) {
            // Store the token object from the post response into the user var locally to use later
            localStorage.setItem('token', user.token);
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            console.log(this.decodedToken);
          }
        })
      );
  }

  // Register method to send the required data into database
  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model); // Model stores data for username and password to be sent
  }

  // Logged in confirmation method to determine status/validity of signed in user
  loggedIn() {
    // Obtain token from localstorage to crosscheck
    const token = localStorage.getItem('token');
    // Check for expiration of token using jwtHelper
    return !this.jwtHelper.isTokenExpired(token); // return true if not expired, return false if expired (to align with existing code)
  }
}
