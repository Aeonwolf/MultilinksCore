﻿import { ErrorHandler, Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ErrorMessage } from '../models/error-message.model';

@Injectable()
export class ErrorsHandler implements ErrorHandler {

   private errorMessage: ErrorMessage;

   constructor() {

      this.errorMessage = new ErrorMessage();
   }

   /* This will be called by an uncaught error */
   handleError(error: Error | HttpErrorResponse) {

      if (error instanceof HttpErrorResponse) {     
         if (!navigator.onLine) {
            console.error("unexpected-connection-error");
            console.error(0);
            return;
         }

         this.errorMessage.errorType = "unexpected-http-error";
         this.errorMessage.errorCode = String(error.status);
      }
      else {
         this.errorMessage.errorType = "unexpected-client-error";
         this.errorMessage.errorCode = "1";
      }

      console.error(this.errorMessage.errorType);
      console.error(this.errorMessage.errorCode);
      this.logError(error);
   }

   /* This will be called when an error is caught */
   handleCaughtError(error: Error | HttpErrorResponse) {

      if (error instanceof HttpErrorResponse) {
         if (!navigator.onLine) {
            this.errorMessage.errorType = "connection-error"
            this.errorMessage.errorCode = "2";
            return this.errorMessage;
         }

         this.errorMessage.errorType = "http-error";
         this.errorMessage.errorCode = String(error.status);
      }
      else {
         this.errorMessage.errorType = "client-error";
         this.errorMessage.errorCode = "3";
      }

      this.logError(error);

      return this.errorMessage;
   }

   logError(error: Error | HttpErrorResponse) {

      console.log("We should log this error somewhere on the server.");
   }
}
