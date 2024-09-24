import { Component, Inject, OnInit} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CustomAlertData } from '@sharedComponents/interface/custom-alert-data';

@Component({
  selector: 'app-custom-alert',
  templateUrl: './custom-alert.component.html',
  styleUrls: ['./custom-alert.component.scss']
})
export class CustomAlertComponent implements OnInit {

  protected customAlertData: CustomAlertData;

  constructor(@Inject(MAT_DIALOG_DATA) public data: CustomAlertData, private ref: MatDialogRef<CustomAlertComponent>){

  }

  ngOnInit(): void {
    this.customAlertData = this.data;
  }

  cerrarAlertConfirm(){
    this.ref.close("confirm");
  }

  cerrarAlertCancel(){
    this.ref.close("cancel");
  }
}
