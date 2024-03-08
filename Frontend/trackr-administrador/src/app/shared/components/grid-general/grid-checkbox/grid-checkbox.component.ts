import { Component, OnInit } from '@angular/core';
import { AgRendererComponent } from 'ag-grid-angular';

@Component({
  selector: 'app-grid-checkbox',
  templateUrl: './grid-checkbox.component.html',
  styleUrls: ['./grid-checkbox.component.css']
})
export class GridCheckboxComponent {
  public params: any;

  agInit(params: any): void {
    this.params = params;
    params.value = params.data.traspaso;
  }

  refresh(params: any): boolean {
    params.data.traspaso = params.value;
    params.api.refreshCells(params);
    return false;
  }
}
