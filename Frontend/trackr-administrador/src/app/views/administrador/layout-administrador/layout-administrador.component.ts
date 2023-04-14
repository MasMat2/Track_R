import { Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-layout-administrador',
  templateUrl: './layout-administrador.component.html',
  styleUrls: ['./layout-administrador.component.css']
})
export class LayoutAdministradorComponent implements OnInit {

  public imagenUsuario = 'assets/img/pruebas/user-image.png';
  public imagenLogotipo = 'assets/img/logo-trackr.png'

  public navItems: any[] = [
    {
      nombre: 'Dashboard',
      claseIcono: 'fas fa-house-medical'
    },
    {
      nombre: 'Pacientes',
      claseIcono: 'fa-regular fa-id-badge'
    },
    {
      nombre: 'Agenda',
      claseIcono: 'fa-regular fa-calendar-days'
    },
    {
      nombre: 'Chat',
      claseIcono: 'fa-regular fa-message'
    },
  ];

  constructor() {

  }

  public ngOnInit(): void {
  }

}
