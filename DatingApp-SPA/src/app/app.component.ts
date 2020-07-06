import { Component } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  jwtHelper = new JwtHelperService();

  constructor(private authService: AuthService) { }

  ngOnInit() {
    // Try to obtain a token stored in the website on startup
    const token = localStorage.getItem('token');
    // If there is a token
    if (token) {
      // Decode token and store in the decoded token var
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }
}
