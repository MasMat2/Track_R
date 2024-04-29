import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { UsuarioWidgetService } from 'src/app/services/dashboard/usuario-widget.service';
import { HeaderComponent } from '../layout/header/header.component';
import { WidgetService } from 'src/app/services/dashboard/widget.service';
import { Router, RouterModule } from '@angular/router'; 
import { ChatHubServiceService } from 'src/app/services/dashboard/chat-hub-service.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    HeaderComponent,
    RouterModule,
  ],
  providers: [
    UsuarioWidgetService,
    WidgetService
  ]
})
export class DashboardPage implements OnInit {

  constructor(
    private ChatHubServiceService:ChatHubServiceService
  ) { }

  public ngOnInit(): void {
    this.ChatHubServiceService.iniciarConexion();
    //this.ChatMensajeHubService.iniciarConexion();
  }
}
