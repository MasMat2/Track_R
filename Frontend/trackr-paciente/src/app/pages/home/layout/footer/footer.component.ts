import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
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

  @ViewChild('tabs') tabs: IonTabs;
  constructor()
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
  }

  public ngOnInit(): void {}

  cambiarIconoTabSeleccionada() {
    this.selectedTab = this.tabs.getSelected();
  }
}
