import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AngularTreeGridComponent } from 'angular-tree-grid';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Acceso } from '@models/seguridad/acceso';
import { RolAcceso } from '@models/seguridad/rol-acceso';
import { RolAccesoService } from '@http/seguridad/acceso-rol.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import * as Utileria from '@utils/utileria';
import { AccesoFormularioComponent } from '../acceso-formulario/acceso-formulario.component';

@Component({
  selector: 'app-reporte-arbol-acceso',
  templateUrl: './reporte-arbol-acceso.component.html',
  styleUrls: ['./reporte-arbol-acceso.component.scss']
})
export class ReporteArbolAccesoComponent implements OnInit {

  @ViewChild('angularGrid', { static: false }) angularGrid: AngularTreeGridComponent;
  public tieneAccesoEditar: boolean = false;
  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  public rolAccesoList: RolAcceso[] = [];
  public accesoList: Acceso[] = [];

  public filtro: any = {};
  public isCollapsed = true;
  public buscando = false;

  public columns = [
    { name: 'nombre', header: 'Nombre', css_class: 'tree-descripcion-cuenta' },
    { name:'tipoAcceso', header: 'Tipo Acceso' },    
    { name:'ordenMenu', header: 'Orden Menú' },
    { name: 'url', header: 'URL' }
  ];

  public configTree: any ={
    id_field: 'idAcceso',
    parent_id_field: 'idAccesoPadre',
    parent_display_field: 'nombre',
    data_loading_text: 'Cargando...',
    css: {
      expand_class: 'fa fa-caret-right',
      collapse_class: 'fa fa-caret-down',
      table_class: 'grid-table-tree'
    },
    columns: this.columns
  }

  public configRolAcceso = Object.assign(
    { labelField: 'nombre', valueField: 'idRolAcceso', searchField: ['nombre'], dropdownParent: 'body' },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  constructor(
    private rolAccesoService: RolAccesoService,
    private accesoService: AccesoService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef
  ) { }

  public ngOnInit(): void {
    this.accesoService.tieneAcceso(CodigoAcceso.EDITAR_ACCESO).subscribe((data) => {
      this.tieneAccesoEditar = data;
    });
    this.consultarRolesAcceso();
  }

  public consultarAccesos(): void {
    this.accesoService.consultarParaReporteArbol(this.filtro.idRolAcceso).subscribe((data) => {
      this.accesoList = data;
      this.buscando = false;
    });
  }

  public consultarRolesAcceso(): void {
    this.rolAccesoService.consultarTodosParaSelector().subscribe((data) => {
      this.rolAccesoList = data;

      const todos = new RolAcceso();
      todos.idRolAcceso = 0;
      todos.nombre = "Todos";
      this.rolAccesoList.unshift(todos);

      this.filtro.idRolAcceso = 0;

      this.consultarAccesos();
    });
  }

  public enviarFormulario(formulario: NgForm): void {
    if (!formulario.valid) {
      Utileria.validarCamposRequeridos(formulario);
      return;
    }

    this.buscando = true;
    this.consultarAccesos();
  }

  public editarAcceso(data: Acceso) {
    if (data.idAcceso > 0 && this.tieneAccesoEditar) {
      this.bsModalRef = this.modalService.show(
        AccesoFormularioComponent,
        GeneralConstant.CONFIG_MODAL_FULL
      );
      this.bsModalRef.content.acceso.idAcceso = data.idAcceso;
      this.bsModalRef.content.esModal = true;
      this.bsModalRef.content.onClose = (cerrar: boolean) => {
        this.bsModalRef.hide();
      };
    }
  }

  public onCellClick(event : any): void {
    this.editarAcceso(event.row);
  }

  public expandAll(): void {
    this.angularGrid.expandAll();
  }

  public collapseAll(): void {
    this.angularGrid.collapseAll();
  }

  public seleccionoPanel(): void {
    this.isCollapsed = !this.isCollapsed;
  }

  public limpiar(): void {
    this.filtro = {};
  }

}
