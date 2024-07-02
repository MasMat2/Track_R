import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule, ModalController } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { UsuarioDoctoresDto } from 'src/app/shared/Dtos/usuario-doctores-dto';
import { chevronBack } from 'ionicons/icons';
import { ChatPersonaService } from '../../../../../shared/http/chat/chat-persona.service';
import { ChatHubServiceService } from '../../../../../services/dashboard/chat-hub-service.service';
import { ChatDTO } from 'src/app/shared/Dtos/Chat/chat-dto';
import { FormsModule } from '@angular/forms';
import { SearchbarComponent } from '@sharedComponents/searchbar/searchbar.component';
import { AlertController } from '@ionic/angular/standalone';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nuevo-chat-doctores',
  templateUrl: './nuevo-chat-doctores.component.html',
  styleUrls: ['./nuevo-chat-doctores.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    FormsModule,
    SearchbarComponent
  ],
  providers : []
})
export class NuevoChatDoctoresComponent  implements OnInit {
  public doctores: UsuarioDoctoresDto[];
  public doctoresFiltrados: UsuarioDoctoresDto[];

  public gruposDoctores: { letra: string, doctores: any[] }[] = [];

  protected usuarios: number[] = [];
  protected tituloChat:string;
  protected doctorSeleccionado: boolean = false;
  protected idDoctorSelecionado:number = 0;
  private idUsuario:number;
  protected filtrando: boolean = false;

  
  constructor(
    private modalCtrl:ModalController,
    private ChatPersonaService:ChatPersonaService,
    private ChatHubServiceService:ChatHubServiceService,
    private alertController: AlertController,
  ) { 
    addIcons({
      'chevron-left': 'assets/img/svg/chevron-left.svg',
      'chevron-right': 'assets/img/svg/chevron-right.svg'
    })
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
    this.obtenerIdUsuario();
  }

  regresarBtn(){
    this.modalCtrl.dismiss();
  }

  private organizarDoctores(docs:UsuarioDoctoresDto[]) {
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

  // protected doctorClick(idDoctor:number){
  //   this.doctorSeleccionado = ! this.doctorSeleccionado
  //   this.idDoctorSelecionado = idDoctor;
  //   this.usuarios = []
  //   this.usuarios.push(this.idUsuario);
  //   this.usuarios.push(idDoctor);
  // }

  protected crearChat(){
    let chat: ChatDTO = {
      fecha: new Date(),
      habilitado: true,
      idCreadorChat: this.usuarios[this.usuarios.length - 1],
      titulo: this.tituloChat,
      idChat: 0
    };

    this.ChatHubServiceService.agregarChat(chat,this.usuarios);
    this.tituloChat = "";
    this.usuarios = []
    this.doctorSeleccionado = false;
  }

  private obtenerIdUsuario(){
    this.ChatPersonaService.obtenerIdUsuario().subscribe(res => {
      this.idUsuario = res;
    })
  }

  protected handleSearch(searchTerm: string): void {
    if( searchTerm == ""){
      this.filtrando = false;
      return
    }
    else{
      this.filtrando = true;
      this.doctoresFiltrados = this.doctores.filter(doctor => 
        doctor.nombre?.toLowerCase().includes(searchTerm.toLowerCase())
      );
      console.log(this.doctoresFiltrados);
    }
  }

  protected async mostrarAlertCrearChat(idDoctor: number){

    const alert = await this.alertController.create({
      header: 'Crear chat',
      subHeader: '¿Desea crear un chat con este doctor?',
      cssClass: 'custom-alert color-primary icon-info two-buttons',
      backdropDismiss: false,
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
        },
        {
          text: 'Crear',
          role: 'confirm',
          handler: () => {

            this.doctorSeleccionado = !this.doctorSeleccionado
            this.idDoctorSelecionado = idDoctor;
            this.usuarios = []
            this.usuarios.push(this.idUsuario);
            this.usuarios.push(idDoctor);

            this.crearChat();
          }
        }
      ],
    });
    await alert.present();
    
    const data = await alert.onDidDismiss();

    if(data.role == "confirm"){
      this.regresarBtn();
    }
  }

}
