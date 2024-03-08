import { Component, ElementRef, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { GridConfig } from './grid-config';
import { GridTab } from './grid-tab';
import { GridGeneralComponent } from '../grid-general/grid-general.component';
import { MatTabChangeEvent } from '@angular/material/tabs';

@Component({
  selector: 'app-grid-filtro',
  templateUrl: './grid-filtro.component.html',
  styleUrls: ['./grid-filtro.component.scss']
})
export class GridFiltroComponent implements OnInit, OnChanges {

  @Input() public gridConfig: GridConfig = {} as GridConfig;
  @Input() public gridData: any[] = [];
  @Input() public gridTabs: GridTab[] = [];

  @Output() gridClick: EventEmitter<{ accion: string, data: any }> = new EventEmitter();

  @ViewChild('slider', { static: true }) slider: ElementRef<HTMLElement>;
  @ViewChild('grid', { static: true }) grid: GridGeneralComponent;

  public selected: FormControl = new FormControl(0);
  public filteredData: any[] = [];

  private tabTodos: GridTab = {
    title: 'Todos',
    filter: {
      field: '',
      criteria: () => true
    }
  };

  constructor() {
  }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['gridTabs']) {
      this.gridTabs = [this.tabTodos, ...changes['gridTabs'].currentValue];
    }

    if (changes['gridData'] || changes['gridTabs']) {
      this.selected = new FormControl(0);

      if (this.gridData && this.gridTabs)
        this.filterData(this.gridTabs[0]);
    }
  }

  public onSelectedTabChange(event: MatTabChangeEvent) {
    const gridTab: GridTab = this.gridTabs[event.index];
    this.filterData(gridTab);
  }

  private filterData(gridTab: GridTab) {
    const filter = gridTab.filter;

    this.filteredData = this.gridData
      .filter(item => filter.criteria(item[filter.field]));
  }

  public emit(event: any): void {
    this.gridClick.emit(event);
  }

  /**
   * Permite modificar la visibilidad de una columna del grid
   * @param nombre field de la columna, para identificarla
   * @param estado true para mostrarla, false para ocultarla
   */
  public modificarVisibilidadColumna(nombre: string, estado: boolean): void {
    this.grid.gridOptions.columnApi.setColumnVisible(nombre, estado);
  }

}
