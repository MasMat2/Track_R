import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs';
import { IonicModule } from '@ionic/angular';
import { IonTabs } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { TabService } from 'src/app/services/dashboard/tab.service';

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

  protected selectedTab: string | undefined;
  protected previousUrl: string;
  protected mostrarFooter: boolean;

  //Rutas donde NO queremos que se muestre el footer (la condición es que la ruta empiece con este string):
  private rutasNoMostrarFooter: string[] = [
    '/home/chat-movil/chat/', 
    '/home/perfil/mis-doctores', 
    '/home/perfil/mis-estudios',
    '/home/perfil/informacion-general',
    '/home/dashboard/seguimiento',
    '/home/perfil/mis-tratamientos',
    '/home/cuestionarios/responder',
    '/home/cuestionarios/ver'
  ];

  @ViewChild('tabs') tabs: IonTabs;
  constructor( private router: Router, private tabService : TabService)
  {
    addIcons({
      'home': 'assets/img/svg/home.svg',
      'home-filled': 'assets/img/svg/home_filled.svg',
      'clipboard-plus': 'assets/img/svg/clipboard-plus.svg',
      'clipboard-plus-filled': 'assets/img/svg/clipboard-plus_filled.svg',
      'message-square-more': 'assets/img/svg/message-square-more.svg',
      'message-square-more-filled': 'assets/img/svg/message-square-more_filled.svg',
      'file-check-2': 'assets/img/svg/file-check-2.svg',
      'file-check-2-filled': 'assets/img/svg/file-check-2_filled.svg',
      'user': 'assets/img/svg/user.svg',
      'user-filled': 'assets/img/svg/user_filled.svg',
    })

    this.verificarCambioEnUrl();
  }

  public ngOnInit(): void {}

  cambiarIconoTabSeleccionada() {
    this.selectedTab = this.tabs.getSelected();
  }

  protected cambiarTab(tabId: string) {
    this.tabService.changeTab(tabId);
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
