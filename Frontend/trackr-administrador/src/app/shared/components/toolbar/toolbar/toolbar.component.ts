import { Component } from '@angular/core';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent {

  handleSearch(searchTerm: string): void {
    console.log(searchTerm);
    // this.filteredItems = this.items.filter(item =>
    //   item.toLowerCase().includes(searchTerm.toLowerCase())
    // );
  }
}
