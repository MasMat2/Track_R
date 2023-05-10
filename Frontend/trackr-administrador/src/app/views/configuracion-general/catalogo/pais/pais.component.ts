import { Component, OnInit } from '@angular/core';
import { PaisService } from '@http/catalogo/pais.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Pais } from '@models/catalogo/pais';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PaisFormularioComponent } from './pais-formulario/pais-formulario.component';

@Component({
  selector: 'app-pais',
  templateUrl: './pais.component.html'
})
export class PaisComponent implements OnInit {
  public tieneAccesoAgregar = false;
  public accesoEditar = CodigoAcceso.EDITAR_PAIS;
  public accesoEliminar = CodigoAcceso.ELIMINAR_PAIS;
  public HEADER_GRID = 'Países';
  private MENSAJE_EXITO_ELIMINAR = 'El país ha sido eliminado';
  private TITULO_MODAL_ELIMINAR = 'Eliminar País';
  public paisList: Pais[];

  public columns = [
    { headerName: 'Clave', field: 'clave', minWidth: 150, width: 70 },
    { headerName: 'Nombre', field: 'nombre', minWidth: 150 }
  ];

  constructor(
    private modalMensajeService: MensajeService,
    private paisService: PaisService,
    private accesoService: AccesoService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef
  ) {}

  ngOnInit(): void {
    this.accesoService.tieneAcceso(CodigoAcceso.AGREGAR_PAIS).subscribe((data) => {
      this.tieneAccesoAgregar = data;
    });

    this.consultarGrid();
  }

  consultarGrid() {
    this.paisService.consultarGeneral().subscribe((data) => {
      this.paisList = data;
    });
  }

  onGridClick(gridData: { accion: string; data: Pais }) {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data.idPais);
    } else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
    }
  }

  agregar() {
    this.bsModalRef = this.modalService.show(PaisFormularioComponent, GeneralConstant.CONFIG_MODAL_DEFAULT);
    this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_AGREGAR;
    this.bsModalRef.content.onClose = (cerrar: boolean) => {
      if (cerrar) {
        this.consultarGrid();
      }
      this.bsModalRef.hide();
    };
  }

  editar(idPais: number) {
    this.paisService.consultar(idPais).subscribe((data) => {
      this.bsModalRef = this.modalService.show(PaisFormularioComponent, GeneralConstant.CONFIG_MODAL_DEFAULT);
      this.bsModalRef.content.pais = data;
      this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_EDITAR;
      this.bsModalRef.content.onClose = (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }
        this.bsModalRef.hide();
      };
    });
  }

  eliminar(pais: Pais) {
    this.modalMensajeService
      .modalConfirmacion(
        '¿Desea eliminar el país <strong>' + pais.nombre + '</strong>?',
        this.TITULO_MODAL_ELIMINAR,
        GeneralConstant.ICONO_CRUZ
      )
      .then((aceptar) => {
        this.paisService.eliminar(pais.idPais).subscribe((data) => {
          this.modalMensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
          this.consultarGrid();
        });
      });
  }
}
