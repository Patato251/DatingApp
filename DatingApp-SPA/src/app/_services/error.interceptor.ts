import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(httperror => { // Pass in the error that is being read
        if (httperror.status === 401) { // If error is 401 error
          return throwError(httperror.statusText); // Throw the error back
        }

        // Looking inside the error object from earlier
        if (httperror instanceof HttpErrorResponse) {
          // Get the application error produced in the header (produced from the API)
          const applicationError = httperror.headers.get('Application-Error'); // These are the 500 internal server error exceptions
          // If there is a error inside this
          if (applicationError) {
            return throwError(applicationError);
          }

          // Modal State errors (e.g. password too short, did not meet required settings for validation)
          const serverError = httperror.error; // This refers to the error tab inside the HttpErrorResponse in console of site
          let modalStateErrors = '';
          // Look for objects inside the http error's error tab (HttpErrorResponse -> error -> error) (Inside console)
          // This refers to the error object within json response e.g. "usernmae" = "Luke"
          if (serverError.errors && typeof serverError.errors === 'object') {
            // Loop to go through each object and print the errors being triggered
            for (const key in serverError.errors) {
              if (serverError.errors[key]) {
                modalStateErrors += serverError.errors[key] + '\n'; // Strings being printed for each error
              }
            }
          }
          // Returns either the modalStateErrors (such as password etc), or the servererror (Username already exists) or a new error
          return throwError(modalStateErrors || serverError || 'Server Error');
        }
      })
    );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true
};
