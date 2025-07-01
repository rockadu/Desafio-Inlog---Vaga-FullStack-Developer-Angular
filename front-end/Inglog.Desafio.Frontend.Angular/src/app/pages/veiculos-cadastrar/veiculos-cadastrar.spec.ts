import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { VeiculosCadastrar } from './veiculos-cadastrar';
import { VeiculosService } from '../../services/veiculos';

describe('VeiculosCadastrar', () => {
  let component: VeiculosCadastrar;
  let fixture: ComponentFixture<VeiculosCadastrar>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        VeiculosCadastrar,
        HttpClientTestingModule
      ],
      providers: [VeiculosService]
    }).compileComponents();

    fixture = TestBed.createComponent(VeiculosCadastrar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
