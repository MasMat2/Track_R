import { Component, OnInit } from '@angular/core';
import { ILoadingCellRendererAngularComp } from 'ag-grid-angular';
import { ILoadingCellRendererParams } from 'ag-grid-community';

@Component({
  selector: 'app-loading-cell-renderer',
  templateUrl: './loading-cell-renderer.component.html',
  styleUrls: ['./loading-cell-renderer.component.scss']
})
export class CustomLoadingCellRenderer implements ILoadingCellRendererAngularComp {

  public params: any;

  agInit(params: ILoadingCellRendererParams): void {
      this.params = params;
  }

}
