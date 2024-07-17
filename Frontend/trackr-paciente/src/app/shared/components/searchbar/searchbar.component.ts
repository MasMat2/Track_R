import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';

@Component({
  selector: 'app-searchbar',
  templateUrl: './searchbar.component.html',
  styleUrls: ['./searchbar.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule
  ]
})
export class SearchbarComponent  implements OnInit {

  @Output() search = new EventEmitter<string>();


  
  constructor() {
    addIcons({
      'arrow-left': 'assets/img/svg/arrow-left.svg',
      'x': 'assets/img/svg/x.svg',
      'search': 'assets/img/svg/search.svg',
    })
   }

  ngOnInit() {

  }

  onSearch(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.search.emit(input.value.trim());
  }
}
