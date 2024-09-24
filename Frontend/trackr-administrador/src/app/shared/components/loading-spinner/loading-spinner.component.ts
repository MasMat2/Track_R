import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-loading-spinner',
  templateUrl: './loading-spinner.component.html',
  styleUrls: ['./loading-spinner.component.scss']
})
export class LoadingSpinnerComponent implements OnInit {

  constructor(@Inject(MatDialogRef) private ref: MatDialogRef<LoadingSpinnerComponent>){}

  ngOnInit() {
  }

  cerrarAlert(){
    this.ref.close();
  }

}
