import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from "@angular/core";
import { GridGeneralComponent } from "@sharedComponents/grid-general/grid-general.component";
import { ColDef, GridOptions } from "ag-grid-community";
import { Observable } from "rxjs";
import { ICrudConfig } from "../crud-base/crud-config";

@Component({
    selector: "app-catalogo-base",
    templateUrl: "./catalogo-base.component.html",
    styleUrls: ["./catalogo-base.component.scss"],
})
export class CatalogoBaseComponent implements OnInit {

    // Expone la referencia al componente grid para poder acceder a sus métodos y configuraciones
    @ViewChild('grid', { static: false }) public grid: GridGeneralComponent;

    // Variables de Entrada
    @Input() public columns: ColDef[];
    @Input() public titulo: string;
    @Input() public tituloBoton: string;
    @Input() public gridOptions?: GridOptions;
    @Input() public crudConfig: ICrudConfig;
    @Input() public elementos$: Observable<any[]>;
    @Input() public tieneAccesoAgregar$: Observable<boolean>;

    // Eventos
    @Output() public gridClick = new EventEmitter<{accion: string, data: any}>();
    @Output() public agregarClick = new EventEmitter();

    // Variables Grid
    public accesoAgregar: string;
    public accesoEditar: string;
    public accesoEliminar: string;

    public disableEdit: boolean = true;
    public disableDelete: boolean = true;

    constructor(
    ) { }

    ngOnInit() {
        this.mapearAccesos();
    }

    private mapearAccesos(): void {
        // Acceso Eliminar
        const configEliminar = this.crudConfig.configEliminar;

        if (configEliminar) {
            this.accesoEliminar = configEliminar.acceso;
            this.disableDelete = false;
        }

        const formConfig = this.crudConfig.formConfig;

        // Acceso Agregar
        // TODO: 2022-12-16 -> Revisar
        // const configAgregar = formConfig && formConfig.configAgregar;

        // if (configAgregar) {
        //     this.accesoAgregar = configAgregar.acceso;
        //     this.consultarAccesoAgregar();
        // }

        // Acceso Editar
        const configEditar = formConfig && formConfig.configEditar;

        if (configEditar) {
            this.accesoEditar = configEditar.acceso;
            this.disableEdit = false;
        }

    }

    public onGridClick(gridData: { accion: string; data: any; }): void {
        this.gridClick.emit(gridData);
    }

    public onAgregarClick(): void {
        this.agregarClick.emit();
    }
}
