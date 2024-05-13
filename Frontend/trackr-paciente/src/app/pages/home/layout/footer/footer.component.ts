import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs';
import { IonicModule } from '@ionic/angular';
import { IonTabs } from '@ionic/angular';
import { addIcons } from 'ionicons';
import {
  home,
  videocam,
  statsChart,
  statsChartOutline,
  documentText,
  documentTextOutline,
  person,
  personOutline,
  chatboxEllipses,
  chatboxEllipsesOutline
} from 'ionicons/icons'

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
  ]
})
export class FooterComponent implements OnInit {

  selectedTab: string | undefined;
  protected previousUrl: string;
  protected mostrarFooter: boolean;

  //Rutas donde NO queremos que se muestre el footer (la condición es que la ruta empiece con este string):
  private rutasNoMostrarFooter: string[] = [
    '/home/chat-movil/chat/', 
    '/home/perfil/mis-doctores', 
    '/home/perfil/mis-estudios',
    '/home/perfil/informacion-general',
    '/home/dashboard/seguimiento',
    '/home/perfil/mis-tratamientos'
  ];

  @ViewChild('tabs') tabs: IonTabs;
  constructor( private router: Router)
  {
    addIcons({
      home,
      videocam,
      statsChart,
      statsChartOutline,
      documentText,
      documentTextOutline,
      person,
      personOutline,
      chatboxEllipses,
      chatboxEllipsesOutline,
    })

    this.verificarCambioEnUrl();
  }

  public ngOnInit(): void {}

  cambiarIconoTabSeleccionada() {
    this.selectedTab = this.tabs.getSelected();
  }

  private verificarCambioEnUrl(){

    this.router.events
    .pipe(filter((event): event is NavigationEnd => event instanceof NavigationEnd))
    .subscribe(async (event: NavigationEnd) =>
     {
      this.mostrarFooter = true;

      const currentUrl = event.urlAfterRedirects;
      const mostrarFooter = !this.rutasNoMostrarFooter.some(ruta => currentUrl.startsWith(ruta));
      
      if(!mostrarFooter){
        this.mostrarFooter = false;
      }

      this.previousUrl = currentUrl;
    });

  }
}
