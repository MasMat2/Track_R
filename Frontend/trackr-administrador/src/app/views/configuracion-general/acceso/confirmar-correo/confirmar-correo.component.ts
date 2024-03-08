import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ConfirmarCorreoDto } from '@dtos/seguridad/confirmar-correo-dto'
import { ConfirmarCorreoService } from '../../../../shared/http/seguridad/confirmar-correo.service'
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-confirmar-correo',
  templateUrl: './confirmar-correo.component.html',
  styleUrls: ['./confirmar-correo.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
  ]
})
export class ConfirmarCorreoComponent implements OnInit {

  datos: ConfirmarCorreoDto = {correo: '', token: ''};
  listo: boolean = false;

  constructor(
    private confirmarCorreoService: ConfirmarCorreoService,
    private route: ActivatedRoute
  ){ }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.datos.correo = params['id'];
      this.datos.token = params['tkn'];
    });
    this.validarConfirmacion();
  }

  private validarConfirmacion(){

    this.confirmarCorreoService.validarCorreo(this.datos).subscribe({
      next: () => {
        this.listo = true;
      }
    })

  }



}
