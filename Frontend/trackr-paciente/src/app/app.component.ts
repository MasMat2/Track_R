import { Component } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { alertCircle, checkmarkCircle } from 'ionicons/icons';

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
    addIcons({alertCircle, checkmarkCircle})
  }
}
