import { Component, OnInit } from '@angular/core';
import { GeneroDto } from '@dtos/catalogo/generoDto';
import { GeneroService } from '@http/catalogo/genero.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Genero } from '@models/catalogo/genero';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { GeneroFormularioComponent } from './genero-formulario/genero-formulario.component';

@Component({
    selector: 'app-genero',
    templateUrl: './genero.component.html'
})

export class GeneroComponent implements OnInit {
    public tieneAccesoAgregar = false;
    public accesoEditar = CodigoAcceso.EDITAR_GENERO;
    public accesoEliminar = CodigoAcceso.ELIMINAR_GENERO;
    public HEADER_GRID = 'Generos';
    private MENSAJE_EXITO_ELIMINAR = 'El genero ha sido eliminado';
    private TITULO_MODAL_ELIMINAR = 'Eliminar Genero';
    public generoList: GeneroDto[];

    public columns = [
        //{headerName: 'ID', field: 'idGenero',  minWidth: 150, width: 70 },
        {headerName: 'Genero', field: 'descripcion', minWidth: 150}
    ];

    constructor(
        private modalMensajeService: MensajeService,
        private generoService: GeneroService,
        private accesoService : AccesoService,
        private modalService: BsModalService,
        private bsModalRef: BsModalRef
    ) {}
    ngOnInit(): void {
        this.accesoService.tieneAcceso(CodigoAcceso.AGREGAR_GENERO).subscribe((data) => {
            this.tieneAccesoAgregar = data;
        });

        this.consultarGrid();
    }

    consultarGrid(){
        this.generoService.consulta().subscribe((data) => {
            this.generoList = data;
        });
    }

    onGridClick(gridData: {accion: string; data: Genero}){
        if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
            this.editar(gridData.data.idGenero);
        }else if(gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR){
            this.eliminar(gridData.data);
        }
    }

    agregar(){
        this.bsModalRef = this.modalService.show(GeneroFormularioComponent, GeneralConstant.CONFIG_MODAL_DEFAULT);
        this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_AGREGAR;
        this.bsModalRef.content.onClose = (cerrar: boolean) => {
        if(cerrar){
                this.consultarGrid();
            }
        this.bsModalRef.hide();
        };
    }
    editar(idGenero: number) {
        this.generoService.consultar(idGenero).subscribe((data) => {
            this.bsModalRef = this.modalService.show(GeneroFormularioComponent, GeneralConstant.CONFIG_MODAL_DEFAULT);
            this.bsModalRef.content.genero = data;
            this.bsModalRef.content.onClose = (cerrar: boolean) => {
                if(cerrar){
                    this.consultarGrid();
                }
                this.bsModalRef.hide();
            };
        });
    }
    eliminar(genero: Genero) {
        this.modalMensajeService
        .modalConfirmacion(
            '¿Desea eliminar el genero <strong>'+genero.descripcion+'</strong>',
            this.TITULO_MODAL_ELIMINAR,
            GeneralConstant.ICONO_CRUZ
            )
            .then((aceptar) => {
                this.generoService.eliminar(genero.idGenero).subscribe((data) => {
                    this.modalMensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
                    this.consultarGrid();
                });
            });
    }


}
