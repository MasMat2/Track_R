import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { ScreenOrientationService } from '@services/screen-orientation.service';
import { AuthService } from 'src/app/auth/auth.service';



@Component({
  selector: 'app-acceso-page',
  templateUrl: './acceso.page.html',
  styleUrls: ['./acceso.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
  ]
})
export class AccesoPage implements OnInit {
  
  constructor(
    private orientacionService: ScreenOrientationService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.orientacionService.lockPortrait();
    this.usuarioLogeado();
  }

  private async usuarioLogeado(){
    const logged = await this.authService.isAuthenticated();

    if(logged){
      this.router.navigateByUrl('/home');
    }
  }
}
