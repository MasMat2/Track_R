import { Component } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
  standalone: true,
  imports: [
    IonicModule
  ]
})
export class AppComponent {
  constructor() {
    addIcons({
      'chevron-left': 'assets/img/svg/chevron-left.svg',
      'info': 'assets/img/svg/info.svg',
    })
  }
}
