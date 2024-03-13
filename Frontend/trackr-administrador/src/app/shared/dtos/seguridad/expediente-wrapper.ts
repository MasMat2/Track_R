import { Domicilio } from '@models/seguridad/domicilio';
import { ExpedientePadecimientoDTO } from '@dtos/seguridad/expediente-padecimiento-dto';
import { ExpedienteTrackR } from '@models/seguridad/expediente-trackr';
import { Usuario } from '@models/seguridad/usuario';
export class ExpedienteWrapper{
    expediente: ExpedienteTrackR;
    paciente: Usuario;
    domicilio: Domicilio;
    padecimientos: ExpedientePadecimientoDTO[];

    constructor(){
        this.expediente = new ExpedienteTrackR();
        this.paciente = new Usuario();
        this.domicilio = new Domicilio();
        this.padecimientos = [];
    }
}