import { Injectable } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';
import { SignalingHubBase } from './signaling-hub';

@Injectable({
  providedIn: 'root',
})
export class SignalingHubService extends SignalingHubBase {
   constructor(authService : AuthService) {
    super('hub/signaling' , authService); 
  }  
}
