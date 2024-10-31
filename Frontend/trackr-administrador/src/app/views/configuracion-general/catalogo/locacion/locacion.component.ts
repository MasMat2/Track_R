import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HospitalService } from '@http/catalogo/hospital.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Hospital } from '@models/catalogo/hospital';
import { EncryptionService } from '@services/encryption.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';

@Component({
  selector: 'app-locacion',
  templateUrl: './locacion.component.html'
})
export class HospitalComponent implements OnInit {
  public tieneAccesoAgregar = false;

  public accesoEditar = CodigoAcceso.EDITAR_HOSPITAL;
  public accesoEliminar = CodigoAcceso.ELIMINAR_HOSPITAL;
  public HEADER_GRID = 'Locacion';
  private MENSAJE_EXITO_ELIMINAR = 'El hospital ha sido eliminado';
  private TITULO_MODAL_ELIMINAR = 'Eliminar hospital';

  public hospitalList: Hospital[] = [];

  public columns = [
    { headerName: 'Núm. Hospital', field: 'idHospital', minWidth: 150, sort: 'asc' },
    { headerName: 'Hospital', field: 'nombre', minWidth: 150 },
    { headerName: 'Ciudad', field: 'ciudad', minWidth: 150 },
    { headerName: 'Gerente', field: 'gerente', minWidth: 150 },
    { headerName: 'Es Predeterminada', field: 'esPredeterminada', minWidth: 150 }
  ];

  constructor(
    private modalMensajeService: MensajeService,
    private hospitalService: HospitalService,
    private accesoService: AccesoService,
    private encryptionService: EncryptionService,
    private router: Router
  ) { }

  ngOnInit() {
    this.accesoService.tieneAcceso(CodigoAcceso.AGREGAR_HOSPITAL).subscribe((data) => {
      this.tieneAccesoAgregar = data;
    });

    this.consultarGrid();
  }

  consultarGrid() {
    this.hospitalService.consultarPorCompaniaParaGrid().subscribe((data) => {
      this.hospitalList = data;
    });
  }

  onGridClick(gridData: { accion: string; data: Hospital }) {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data.idHospital);
    } else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
    }
  }

  agregar() {
    this.router.navigate(['administrador/configuracion-general/catalogo/locacion/form'], {
      queryParams: this.encryptionService.generateURL({
        accion: GeneralConstant.MODAL_ACCION_AGREGAR
      })
    });
  }

  editar(idHospital: number) {
    this.hospitalService.consultar(idHospital).subscribe((data) => {
      this.router.navigate(['administrador/configuracion-general/catalogo/locacion/form'], {
        queryParams: this.encryptionService.generateURL({
          data: JSON.stringify(data),
          accion: GeneralConstant.MODAL_ACCION_EDITAR
        })
      });
    });
  }

  eliminar(hospital: Hospital) {
    this.modalMensajeService
      .modalConfirmacion(
        '¿Desea eliminar el hospital <strong>' + hospital.nombre + '</strong>?',
        this.TITULO_MODAL_ELIMINAR,
        GeneralConstant.ICONO_CRUZ
      )
      .then((aceptar) => {
        this.hospitalService.eliminar(hospital.idHospital).subscribe((data) => {
          this.modalMensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
          this.consultarGrid();
        });
      });
  }
}
