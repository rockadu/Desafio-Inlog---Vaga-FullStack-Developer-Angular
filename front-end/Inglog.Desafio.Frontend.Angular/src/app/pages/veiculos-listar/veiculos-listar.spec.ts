import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing'; // ✅ Importa o módulo de testes do HttpClient
import { VeiculosListar } from './veiculos-listar';

describe('VeiculosListar', () => {
  let component: VeiculosListar;
  let fixture: ComponentFixture<VeiculosListar>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        VeiculosListar,
        HttpClientTestingModule // ✅ Injeta dependências do HttpClient para o serviço
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(VeiculosListar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
