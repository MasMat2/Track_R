import { Component, ContentChild, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-modal-base',
  templateUrl: './modal-base.component.html',
  styleUrls: ['./modal-base.component.scss']
})
export class ModalBaseComponent implements OnInit {
  @ContentChild('modalBody', { static: false }) modalBody: TemplateRef<any>;
  @ContentChild('modalFooter', { static: false }) modalFooter: TemplateRef<any>;

  @Input() public titulo: string;
  @Output() public onClose = new EventEmitter<boolean>();

  constructor(
    public modalRef: BsModalRef
  ) { }
 
  ngOnInit(): void {
  }

  public cerrar() {
    this.onClose.emit(true);
    this.modalRef.hide();
  }
}
