import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { FooterComponent } from './layout/footer/footer.component';
// import { BreadcrumbModule } from 'angular-crumbs';

@Component({
  selector: 'app-home-page',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    FooterComponent/* ,
    BreadcrumbModule */
  ]
})
export class HomePage implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
