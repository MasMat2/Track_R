import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IonicModule, PopoverController } from '@ionic/angular';
import { GeneralConstant } from '@utils/general-constant';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css'],
  standalone: true,
  imports: [  
    IonicModule
  ]
})
export class LogoutComponent implements OnInit {

  constructor(
    private router: Router,
    private popoverController : PopoverController,
    private authService : AuthService
  ) { }

  ngOnInit() {
  }

  public logout(): void {
    this.authService.cerrarSesion();
    this.popoverController.dismiss();
    
  }

}
