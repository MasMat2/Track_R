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

@Component({
  selector: 'app-nuevo-chat-doctores',
  templateUrl: './nuevo-chat-doctores.component.html',
  styleUrls: ['./nuevo-chat-doctores.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    FormsModule
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

  
  constructor(private modalCtrl:ModalController,
              private ChatPersonaService:ChatPersonaService,
              private ChatHubServiceService:ChatHubServiceService) 
  { 
    addIcons({chevronBack})
  }

  ngOnInit() {
    //this.doctores.push({nombre:'Arturo Mesa',hospital:'',ambito: '',idExpediente:0,idExpedienteDoctor:0,idUsuarioDoctor:0,imagenBase64:'',tipoMime:''})
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

  doctorClick(idDoctor:number){
    this.doctorSeleccionado = ! this.doctorSeleccionado
    this.idDoctorSelecionado = idDoctor;
    this.usuarios = []
    this.usuarios.push(this.idUsuario);
    this.usuarios.push(idDoctor);
  }

  crearChat(){
    let chat: ChatDTO = {
      fecha: new Date(),
      habilitado: true,
      idCreadorChat: this.usuarios[this.usuarios.length - 1],
      titulo: this.tituloChat,
      idChat: 0
    };

    //this.ChatHubServiceService.iniciarConexion();

    this.ChatHubServiceService.agregarChat(chat,this.usuarios);
    this.tituloChat = "";
    this.usuarios = []
    this.doctorSeleccionado = false;
  }

  obtenerIdUsuario(){
    this.ChatPersonaService.obtenerIdUsuario().subscribe(res => {
      this.idUsuario = res;
    })
  }

}
