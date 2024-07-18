import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LoadingSpinnerComponent } from '@sharedComponents/loading-spinner/loading-spinner.component';

@Injectable({
  providedIn: 'root'
})
export class LoadingSpinnerService {

constructor(private dialog: MatDialog) { }

  openSpinner(){
    const alert = this.dialog.open(LoadingSpinnerComponent, {
      panelClass: 'loading-alert',
      disableClose: true
    });

    alert.beforeClosed().subscribe(result => {
    } );
  }

  closeSpinner(){
    this.dialog.closeAll();
  }

}
