import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor() { }

  // Confirmation message passing through message, with a cancel event option
  confirm(message: string, okCallback: () => any) {
    alertify.confirm(message, (e: any) => {  // setting event to any to be triggered (button, timeout etc)
      if (e) {
        okCallback();
      } else { }
    });
  }

  // Sucess Message passing through string
  success(message: string) {
    alertify.success(message);
  }

  // Error Message passing through string
  error(message: string) {
    alertify.error(message);
  }

  // Warning Message passing through string
  warning(message: string) {
    alertify.warning(message);
  }

  // Customised Message passing through string
  message(message: string) {
    alertify.message(message);
  }
}
