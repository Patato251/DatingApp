import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  // values: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    // this.getValues();
  }

  // Toggle on when register button clicked as cancel reigster now toggles false 
  registerToggle() {
    this.registerMode = true;
  }

  // Obtains values from the API in the background and assigns the grabbed values into values variable
  // getValues() {
  //   this.http.get('http://localhost:5000/api/values').subscribe(response => {
  //     this.values = response;
  //   }, error => {
  //     console.log(error);
  //   }); // for testing purposes of hardcoded address
  // }

  // Cancel Register Method
  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode; // Assigns this classes register mode to the value received from the child
  }
}
