import { AfterContentInit, AfterViewInit, Component, ComponentRef, Input, OnInit, Type, ViewChild, ViewContainerRef } from '@angular/core';
import { ExpedientePadecimientoComponent } from 'src/app/views/gestion-paciente/paciente/expediente-padecimiento/expediente-padecimiento.component';

@Component({
  selector: 'app-tab',
  templateUrl: './tab.component.html',
  styleUrls: ['./tab.component.scss']
})
export class TabComponent implements AfterContentInit {

  @Input() public component: Type<any>;
  @Input() public args: { [key: string]: any };

  constructor(
    private viewContainerRef: ViewContainerRef
  ) { }

  ngAfterContentInit() {
    const ref = this.viewContainerRef.createComponent(this.component);

    type ObjectKey = keyof typeof ref.instance;

    for (const key in this.args) {
      const variable = key as ObjectKey;
      // TODO: 2023-07-05 -> Agregar validaciones para llaves que no existan
      ref.instance[variable] = this.args[key];
    }

  }

}
