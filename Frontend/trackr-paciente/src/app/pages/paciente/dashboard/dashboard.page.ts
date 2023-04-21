import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { WidgetContainerComponent } from './components/widget-container/widget-container.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    WidgetContainerComponent,
  ]
})
export class DashboardPage implements OnInit {

  constructor(
  ) { }

  public ngOnInit(): void {
  }

}
