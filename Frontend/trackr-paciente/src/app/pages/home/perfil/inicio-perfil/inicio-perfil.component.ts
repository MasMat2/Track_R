import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { IonicModule } from '@ionic/angular';

@Component({
  selector: 'app-inicio-perfil',
  templateUrl: './inicio-perfil.component.html',
  styleUrls: ['./inicio-perfil.component.scss'],
  standalone: true,
  imports : [
    IonicModule,
    RouterModule,
  ]
})
export class InicioPerfilComponent  implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {}


  protected navigateInformacionGeneral(){
    this.router.navigateByUrl("home/perfil/informacion-general");
  }

  protected navigateMisDoctores(){
    this.router.navigateByUrl("home/perfil/mis-doctores");
  }

  protected navigateMisEstudios(){
    this.router.navigateByUrl("home/perfil/mis-estudios");
  }

  protected navigateMisTratamientos(){
    this.router.navigateByUrl("home/perfil/mis-tratamientos");
  }

  protected cerrarSesion(){

  }

}
