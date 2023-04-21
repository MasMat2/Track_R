import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GeneralConstant } from '@shared/general-constant';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {
  isActionSheetOpen = false;
  public actionSheetButtons = [
    {
      text: 'Cerrar Sesión',
      role: 'destructive',
      icon:'log-out',
      data: {
        action: 'logout'
      }
    },
    {
      text: 'Ver Perfil',
      icon:'person',
      data: {
        action: 'seeProfile'
      }
    },
    {
      text: 'Cancelar',
      role: 'cancel',
      icon:'close',
      data: {
        action: 'cancel'
      }
    }
  ];
  constructor(
    private router: Router)
  {}

  public ngOnInit(): void {}

  public cerrarSesion(): void {
    localStorage.removeItem(GeneralConstant.TOKEN_KEY);
    this.router.navigate(['/login']);
  }

  setOpen(isOpen: boolean) {
    this.isActionSheetOpen = isOpen;
  }

  setResult(event: any) {
    switch ((<CustomEvent>event).detail.data?.action) {
      case 'logout':
        this.cerrarSesion();
        break;
      case 'seeProfile':
        this.router.navigate(['/perfil']); //TODO: Reemplazar la ruta por la ruta del perfil
        break;
      default:
        break;
    }
  }
}
