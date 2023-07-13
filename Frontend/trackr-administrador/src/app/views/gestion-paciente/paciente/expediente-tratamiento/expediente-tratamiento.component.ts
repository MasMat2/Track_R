import { GeneralConstant } from '@utils/general-constant';
import { first } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExpedienteTratamientoGridDTO } from '@dtos/gestion-expediente/expediente-tratamiento-grid-dto';
import { ExpedienteTratamientoService } from '@http/gestion-expediente/expediente-tratamiento.service';
import { ExpedienteTratamiento } from '@models/gestion-expediente/expediente-tratamiento';
import { EncryptionService } from '@services/encryption.service';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-expediente-tratamiento',
  templateUrl: './expediente-tratamiento.component.html',
  styleUrls: ['./expediente-tratamiento.component.scss']
})
export class ExpedienteTratamientoComponent implements OnInit {

  protected expedienteTratamientoList: ExpedienteTratamientoGridDTO[] = [];
  protected tratamiento: ExpedienteTratamiento;
  protected urlImagen = "";
  // protected pdfSrc: any;
  protected idUsuario: number;


  constructor(
    private expedienteTratamientoService: ExpedienteTratamientoService,
    private route: ActivatedRoute,
    private encryptionService: EncryptionService
  ) { }

  ngOnInit() {
    this.expedienteTratamientoService.agregar().subscribe();
    this.obtenerParametrosURL();
  }

  consultarTratamientos(){
    lastValueFrom(this.expedienteTratamientoService.consultarPorUsuario(this.idUsuario))
    .then((tratamientoPacienteList: ExpedienteTratamientoGridDTO[]) => {
      this.expedienteTratamientoList = tratamientoPacienteList;
    });
  }

  /**
   * Obtiene los parámetros de la URL y los asigna a las variables del componente.
   */
  private async obtenerParametrosURL(): Promise<void> {
    const queryParams = await lastValueFrom(this.route.queryParams.pipe(first()));
    const params = this.encryptionService.readUrlParams(queryParams);
    this.idUsuario = Number(params.i);
    this.consultarTratamientos();
  }

}
