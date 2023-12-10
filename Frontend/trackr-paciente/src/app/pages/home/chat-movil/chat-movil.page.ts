import { Component, OnInit } from '@angular/core';
import { Header } from 'primeng/api';
import { HeaderComponent } from '../layout/header/header.component';
import { IonicModule } from '@ionic/angular';

@Component({
  selector: 'app-chat-movil',
  templateUrl: './chat-movil.page.html',
  styleUrls: ['./chat-movil.page.scss'],
  standalone: true,
  imports: [
    HeaderComponent,
  IonicModule]
})
export class ChatMovilComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
