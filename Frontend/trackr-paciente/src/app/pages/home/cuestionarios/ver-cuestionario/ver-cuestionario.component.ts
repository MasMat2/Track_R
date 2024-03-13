import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ExamenReactivoService } from '@http/cuestionarios/examen-reactivo.service';
import { ExamenService } from '@http/cuestionarios/examen.service';
import { AlertController, IonicModule } from '@ionic/angular';
import { Examen } from '@models/examen/examen';
import { ExamenReactivo } from '@models/examen/examen-reactivo';
import { HeaderComponent } from '@pages/home/layout/header/header.component';

@Component({
  selector: 'app-ver-cuestionario',
  templateUrl: './ver-cuestionario.component.html',
  styleUrls: ['./ver-cuestionario.component.scss'],
  standalone: true,
  imports: [
    IonicModule, 
    CommonModule,
    FormsModule,
    HeaderComponent,
    RouterModule
  ]
})
export class VerCuestionarioComponent  implements OnInit {

  private idExamen: any;
  private examen = new Examen();
  protected tipoExamen: string;
  protected reactivoList: ExamenReactivo[] = [];

  constructor(
    private route: ActivatedRoute,
    private examenService: ExamenService,
    private examenReactivoService: ExamenReactivoService,
    private router: Router,
  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.idExamen = params.get('id');
    });
  }

  ionViewWillEnter(){
    if(this.idExamen > 0){
      this.consultarExamen(this.idExamen);
    }
  }


  consultarExamen(idExamen: number) {
    this.examenService.consultarMiExamen(idExamen).subscribe((data) => {
      this.examen = data;
      this.tipoExamen = this.examen.tipoExamen;
      this.consultarReactivos();
    });
  }

  consultarReactivos() {
    this.examenReactivoService
      .consultarReactivosExamen(this.examen.idExamen)
      .subscribe((data) => {
        if (data.length != this.examen.totalPreguntas) {
          this.cancelar();
        } else {
          this.reactivoList = data;
        }
      });
  }

  public cancelar(): void {
    this.router.navigate(['/home/cuestionarios/misCuestionarios'], {});
  }

}
