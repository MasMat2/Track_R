import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AgregarTratamientoPage } from './agregar-tratamiento.page';

describe('AgregarTratamientoPage', () => {
  let component: AgregarTratamientoPage;
  let fixture: ComponentFixture<AgregarTratamientoPage>;

  beforeEach(async(() => {
    fixture = TestBed.createComponent(AgregarTratamientoPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
