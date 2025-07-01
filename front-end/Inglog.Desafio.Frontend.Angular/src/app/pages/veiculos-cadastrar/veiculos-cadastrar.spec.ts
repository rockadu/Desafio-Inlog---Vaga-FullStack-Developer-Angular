import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VeiculosCadastrar } from './veiculos-cadastrar';

describe('VeiculosCadastrar', () => {
  let component: VeiculosCadastrar;
  let fixture: ComponentFixture<VeiculosCadastrar>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VeiculosCadastrar]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VeiculosCadastrar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
