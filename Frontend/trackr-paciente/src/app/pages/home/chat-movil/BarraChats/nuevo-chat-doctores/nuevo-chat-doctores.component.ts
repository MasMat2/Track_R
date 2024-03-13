import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule, ModalController } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { UsuarioDoctoresDto } from 'src/app/shared/Dtos/usuario-doctores-dto';
import { chevronBack } from 'ionicons/icons';

@Component({
  selector: 'app-nuevo-chat-doctores',
  templateUrl: './nuevo-chat-doctores.component.html',
  styleUrls: ['./nuevo-chat-doctores.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule
  ],
  providers : []
})
export class NuevoChatDoctoresComponent  implements OnInit {
  public doctores: UsuarioDoctoresDto[];
  public doctoresFiltrados: UsuarioDoctoresDto[];

  public gruposDoctores: { letra: string, doctores: any[] }[] = [];

  
  constructor(private modalCtrl:ModalController) 
  { 
    addIcons({chevronBack})
  }

  ngOnInit() {
    this.doctores.sort( (a,b) => {
      if(a.nombre > b.nombre){
        return 1;
      }
      else{
        return -1
      }
      return 0
    })

    this.organizarDoctores(this.doctores);
  }

  regresarBtn(){
    this.modalCtrl.dismiss();
  }

  organizarDoctores(docs:UsuarioDoctoresDto[]) {
    const grupos: { [letra: string]: any[] } = {};

    docs.forEach(doctor => {
      const primeraLetra = doctor.nombre.charAt(0).toUpperCase();
      if (!grupos[primeraLetra]) {
        grupos[primeraLetra] = [];
      }
      grupos[primeraLetra].push(doctor);
    });

    this.gruposDoctores = Object.keys(grupos).map(letra => ({
      letra,
      doctores: grupos[letra]
    }));
  }

  protected buscar(event: any){
    const text = event.target.value;
    this.doctoresFiltrados = this.doctores;
    if(text && text.trim() != ''){
      let filter = this.doctoresFiltrados.filter((doc) =>{
          return (doc.nombre.toLowerCase().indexOf(text.toLowerCase()) > -1 );
      })

      this.organizarDoctores(filter);
    }else{
      this.organizarDoctores(this.doctores)
    }
  }

}
