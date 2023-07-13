import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpedienteRecomendacionComponent } from './expediente-recomendacion.component';

describe('ExpedienteRecomendacionComponent', () => {
  let component: ExpedienteRecomendacionComponent;
  let fixture: ComponentFixture<ExpedienteRecomendacionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExpedienteRecomendacionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExpedienteRecomendacionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
