import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { GeneroService } from '@http/catalogo/genero.service';
import { GeneroDto } from '@dtos/catalogo/generoDto';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef } from 'ngx-bootstrap/modal';
import * as Utileria from '@utils/utileria';

@Component({
    selector: 'app-genero-formulario',
    templateUrl: './genero-formulario.component.html',
})
export class GeneroFormularioComponent implements OnInit {
    protected placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
    public onClose: any;
    public accion: string;
    private mensajeAgregar = 'El género ha sido agregado';
    private mensajeEditar = 'El género ha sido modificado';
    protected btnSubmit = false;
    protected genero = new GeneroDto();
    
    constructor(
        public bsModalRef: BsModalRef,
        private modalMensajeService: MensajeService,
        private generoService: GeneroService,
    ) {}

    ngOnInit() {}

    limpiarFormulario(): void {
        this.genero = new GeneroDto();
        this.accion = GeneralConstant.MODAL_ACCION_AGREGAR;
    }

    agregar(formulario:NgForm){
        this.generoService.agregar(this.genero).subscribe(
            data => {
                this.modalMensajeService.modalExito(this.mensajeAgregar);
                formulario.reset();
                this.limpiarFormulario();
                this.onClose(true);
                this.btnSubmit = false;
            },

            error => {
                this.btnSubmit = false;
            }
        );
    }

    editar(){
        this.generoService.editar(this.genero).subscribe(
            data => {
                this.modalMensajeService.modalExito(this.mensajeEditar);
                this.onClose(true);
            },
            error => {
                this.btnSubmit = false;
            }
        );
    }

    cancelar(){
        this.onClose(true);
    }

    enviarFormulario(formulario: NgForm){
        this.btnSubmit = true;
        if(!formulario.valid){
            Utileria.validarCamposRequeridos(formulario);
            this.btnSubmit = false;
            return;
        }

        if(this.accion === GeneralConstant.MODAL_ACCION_AGREGAR){
            this.agregar(formulario);
        }else if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR) {
            this.editar();
        }
    }
}