import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { ScreenOrientationService } from '@services/screen-orientation.service';



@Component({
  selector: 'app-acceso-page',
  templateUrl: './acceso.page.html',
  styleUrls: ['./acceso.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    RouterLink
  ]
})
export class AccesoPage implements OnInit {
  
  constructor(private orientacionService: ScreenOrientationService) { }

  ngOnInit() {
    this.orientacionService.lockPortrait();
  }

}
