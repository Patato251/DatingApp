import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // @Input() valuesFromHome: any;
  @Output() cancelRegister = new EventEmitter;
  model: any = {};

  constructor(private authSerivce: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  // Register method calles the register method inside authorise service and subscribes with it
  // in order to process the observable that is posted by the register method in AuthService
  register() {
    this.authSerivce.register(this.model).subscribe(() => {
      this.alertify.success('Registration has been completed');
    }, error => {
      this.alertify.error(error);
    })
  }

  cancel() {
    // When triggered, set the register method value to false, therefore turning off register 
    this.cancelRegister.emit(false);
    console.log('Cancelled');
  }
}
