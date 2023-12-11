import { Usuario } from "@models/usuario";
import { ExpedientePadecimientoDTO } from '../../../../../../trackr-administrador/src/app/shared/dtos/seguridad/expediente-padecimiento-dto';
import { Domicilio } from '../../../../../../trackr-administrador/src/app/shared/models/seguridad/domicilio';
import { ExpedienteTrackR } from '../../../../../../trackr-administrador/src/app/shared/models/seguridad/expediente-trackr';

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