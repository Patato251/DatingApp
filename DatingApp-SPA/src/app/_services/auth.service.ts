import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';

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
          }
        })
      );
  }

  // Register method to send the required data into database
  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model); // Model stores data for username and password to be sent
  }
}
