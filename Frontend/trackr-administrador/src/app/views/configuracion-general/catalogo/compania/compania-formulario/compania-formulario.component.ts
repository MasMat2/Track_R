import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EncryptionService } from '@services/encryption.service';
import { first } from 'rxjs/operators';
import { GeneralConstant } from 'src/app/shared/utils/general-constant';

@Component({
  selector: 'app-compania-formulario',
  templateUrl: './compania-formulario.component.html',
  styleUrls: ['./compania-formulario.component.scss']
})
export class CompaniaFormularioComponent implements OnInit {
  public accion: string;
  public idCompania: number;

	public renderChildren: boolean = false;

  constructor(
    private encryptionService: EncryptionService,
    private route: ActivatedRoute
  ) { }

  async ngOnInit() {
    const queryParams = await this.route.queryParams.pipe(first()).toPromise();
    const params = this.encryptionService.readUrlParams(queryParams);

    this.accion = params.accion;
    this.idCompania = params.idCompania;

    this.renderChildren = true;
  }

  public esEditar(): boolean {
    return this.accion === GeneralConstant.MODAL_ACCION_EDITAR;
  }
}
