import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VeiculosListar } from './veiculos-listar';

describe('VeiculosListar', () => {
  let component: VeiculosListar;
  let fixture: ComponentFixture<VeiculosListar>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VeiculosListar]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VeiculosListar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
