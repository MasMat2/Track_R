import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ITreeOptions, TREE_ACTIONS, TreeComponent, TreeNode } from '@circlon/angular-tree-component';
import { AccesoService } from '@http/seguridad/acceso.service';
import { JerarquiaAccesoEstructuraService } from '@http/seguridad/jerarquia-acceso-estructura.service';
import { JerarquiaAccesoService } from '@http/seguridad/jerarquiaAcceso.service';
import { PerfilService } from '@http/seguridad/perfil.service';
import { JerarquiaEstructuraArbol } from '@models/contabilidad/jerarquia-estructura-arbol';
import { Acceso } from '@models/seguridad/acceso';
import { JerarquiaAcceso } from '@models/seguridad/jerarquia-acceso';
import { Perfil } from '@models/seguridad/perfil';
import { EncryptionService } from '@services/encryption.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { first } from 'rxjs/operators';

@Component({
  templateUrl: 'perfil-formulario.component.html'
})
export class PerfilFormularioComponent implements OnInit {
  @ViewChild('tree', { static: false }) treeView: TreeComponent;

  public MENSAJE_AGREGAR = 'El perfil ha sido agregado';
  public MENSAJE_EDITAR = 'El perfil ha sido modificado';
  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  public perfil = new Perfil();

  public accesoList: Acceso[] = [];
  public idsAccesosSeleccionados: number[] = [];
  public jerarquiaAccesoList: JerarquiaAcceso[] = [];
  public jerarquiaArbolList: JerarquiaEstructuraArbol[] = [];

  public titulo = 'Alta';
  public btnSubmit = false;

  // Configuracione default de selects
  public options: ITreeOptions = {
    displayField: 'nombre',
    isExpandedField: 'expanded',
    idField: 'idAcceso',
    childrenField: 'hijos',
    useCheckbox: true,
    useTriState: false,
    actionMapping: {
      mouse: {
        dblClick: TREE_ACTIONS.TOGGLE_EXPANDED
      }
    }
  };

  public optionsJerarquia: ITreeOptions = {
    displayField: 'cuenta',
    isExpandedField: 'expanded',
    idField: 'idAcceso',
    childrenField: 'hijos',
    useCheckbox: false,
    useTriState: false,
    actionMapping: {
      mouse: {
        dblClick: TREE_ACTIONS.TOGGLE_EXPANDED
      }
    }
  };

  public idJerarquiaAcceso?: number;

  constructor(
    private modalMensajeService: MensajeService,
    private router: Router,
    private route: ActivatedRoute,
    private perfilService: PerfilService,
    private encryptionService: EncryptionService,
    private accesoService: AccesoService,
    private jerarquiaAccesoService: JerarquiaAccesoService,
    private jerarquiaAccesoEstructuraService: JerarquiaAccesoEstructuraService
  ) {}

  public async ngOnInit(): Promise<void> {
    const queryParams = await this.route.queryParams.pipe(first()).toPromise();
    const params = this.encryptionService.readUrlParams(queryParams);
    const idJerarquiaAcceso = Number(params.ij);
    const idPerfil = Number(params.i);

    this.perfil.idJerarquiaAcceso = idJerarquiaAcceso > 0 ? idJerarquiaAcceso : 0;
    this.perfil.idPerfil = idPerfil > 0 ? idPerfil : -1;

    await Promise.all([
      this.consultarJerarquiaArbolEstructura(idJerarquiaAcceso),
    ]);

    this.consultarPerfil();
    this.consultarJerarquiasAcceso();
  }

  private consultarJerarquiasAcceso() {
    this.jerarquiaAccesoService.consultarParaSelector().subscribe((data) => {
      this.jerarquiaAccesoList = data;
    });
  }

  public async consultarJerarquiaArbolEstructura(idJerarquiaAcceso: number): Promise<void> {
    this.perfil.idJerarquiaAcceso = idJerarquiaAcceso ?? 0;

    this.idsAccesosSeleccionados = [];
    this.jerarquiaArbolList = [];

    if (idJerarquiaAcceso > 0) {
      return this.jerarquiaAccesoEstructuraService.consultarArbol(idJerarquiaAcceso)
      .toPromise()
      .then((data) => {
        this.jerarquiaArbolList = data ?? [];
      });
    }
  }

  private consultarPerfil() {
    if (this.perfil.idPerfil > 0) {
      this.perfilService.consultar(this.perfil.idPerfil).subscribe((data) => {
        this.perfil = data;
        this.idsAccesosSeleccionados = data.idsAcceso;
        this.titulo = 'Modificar';

        this.idsAccesosSeleccionados.forEach((idAcceso) => {
          let node: TreeNode = this.treeView.treeModel.getNodeById(`${idAcceso}`);
          if (node) {
            this.treeView.treeModel.setSelectedNode(node, true);
          }
        });
      });
    }
  }

  public onClickCheckbox($event: TreeNode) {
    if (!this.idsAccesosSeleccionados.some(idAcceso => idAcceso == $event.data.idAcceso)) {
      this.idsAccesosSeleccionados.push($event.data.idAcceso);
    } else {
      this.idsAccesosSeleccionados = this.idsAccesosSeleccionados.filter(idAcceso => idAcceso != $event.data.idAcceso);
    }
  }

  public enviarFormulario(formulario: NgForm) {
    this.btnSubmit = true;
    this.perfil.idsAcceso = this.idsAccesosSeleccionados;

    if (!formulario.valid) {
      this.btnSubmit = false;
      this.validarCamposRequeridos(formulario);
      return;
    }

    if (this.perfil.idPerfil > 0) {
      this.editar();
    } else {
      this.agregar(formulario);
    }
  }

  public agregar(formulario: NgForm) {
    this.perfilService.agregar(this.perfil).subscribe(
      (data) => {
        this.modalMensajeService.modalExito(this.MENSAJE_AGREGAR);
        this.limpiarFormulario(formulario);
        this.btnSubmit = false;
      },
      (error) => {
        this.btnSubmit = false;
      }
    );
  }

  public editar() {
    this.perfilService.editar(this.perfil).subscribe(
      (data) => {
        this.modalMensajeService.modalExito(this.MENSAJE_EDITAR);
        this.btnSubmit = false;
        this.router.navigate(['/administrador/configuracion-general/catalogo/perfil']);
      },
      (error) => {
        this.btnSubmit = false;
      }
    );
  }

  public seleccionarTodo() {
    if (this.perfil.idJerarquiaAcceso)
      this.idsAccesosSeleccionados = this.jerarquiaArbolList.map(a => a.idAcceso);
    else
      this.idsAccesosSeleccionados = this.accesoList.map(a => a.idAcceso);

    this.idsAccesosSeleccionados.forEach((idAcceso) => {
      let node: TreeNode = this.treeView.treeModel.getNodeById(`${idAcceso}`);
      if (node) {
        this.treeView.treeModel.setSelectedNode(node, true);
        this.seleccionarHijos(node.data.hijos);
      }
    });
  }

  public seleccionarHijos(hijos: any[]): void {

    if (
      hijos === undefined ||
      hijos === null ||
      hijos.length === 0
    ) {
      return;
    }

    hijos.forEach((a) => {
      let node: TreeNode = this.treeView.treeModel.getNodeById(`${a.idAcceso}`);

      if (node) {
        this.treeView.treeModel.setSelectedNode(node, true);

        if (!this.idsAccesosSeleccionados.some(idAcceso => idAcceso == a.idAcceso)) {
          this.idsAccesosSeleccionados.push(a.idAcceso);
        }

        this.seleccionarHijos(a.hijos);
      }
    });
  }

  public limpiarFormulario(formulario: NgForm) {
    formulario.reset();
    this.perfil = new Perfil();
    this.idsAccesosSeleccionados = [];
  }

  public cancelar() {
    this.router.navigate(['/administrador/configuracion-general/catalogo/perfil']);
  }

  /**
   * Marca en rojo las validaciones de campos requeridos
   */
  private validarCamposRequeridos(formulario: any) {
    Object.keys(formulario.controls).forEach((nombre) => {
      const control = formulario.controls[nombre];
      control.markAsTouched({ onlySelf: true });
    });
  }
}
