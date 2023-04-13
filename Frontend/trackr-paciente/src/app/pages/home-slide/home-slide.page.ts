import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home-slide',
  templateUrl: './home-slide.page.html',
  styleUrls: ['./home-slide.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, RouterLink]
})
export class HomeSlidePage implements OnInit {
  constructor() { }

  ngOnInit() {
  }

}
