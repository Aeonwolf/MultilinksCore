﻿import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';

@Component({
   selector: 'shared-header',
   templateUrl: './header.component.html'
})

export class HeaderComponent {

   constructor(private service: AuthService) {
   }

   register() {
      this.service.registerUser();
   }
}

