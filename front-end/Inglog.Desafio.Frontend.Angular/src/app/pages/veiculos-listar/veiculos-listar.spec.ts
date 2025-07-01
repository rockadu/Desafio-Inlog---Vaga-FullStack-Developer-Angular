import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { VeiculosListar } from './veiculos-listar';
import { VeiculosService } from '../../services/veiculos';
import { of } from 'rxjs';

describe('VeiculosListar', () => {
  let component: VeiculosListar;
  let fixture: ComponentFixture<VeiculosListar>;
  let veiculosServiceSpy: jasmine.SpyObj<VeiculosService>;

  const veiculosMock = [
    {
      chassi: '123',
      placa: 'ABC-1234',
      coordenadas: { latitude: -25.43, longitude: -49.27 },
      tipoVeiculo: 1,
      cor: 'blue'
    }
  ];

  beforeEach(async () => {
    veiculosServiceSpy = jasmine.createSpyObj('VeiculosService', ['listar', 'deletar']);
    veiculosServiceSpy.listar.and.resolveTo(veiculosMock);

    await TestBed.configureTestingModule({
      imports: [
        VeiculosListar,
        HttpClientTestingModule
      ],
      providers: [
        { provide: VeiculosService, useValue: veiculosServiceSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(VeiculosListar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('deve chamar o método listar do serviço ao inicializar', async () => {
    // simula localização
    spyOn(navigator.geolocation, 'getCurrentPosition').and.callFake((success: any) => {
      success({
        coords: { latitude: -25.4284, longitude: -49.2733 }
      });
    });

    await component.ngAfterViewInit();
    expect(veiculosServiceSpy.listar).toHaveBeenCalled();
  });

  it('deve filtrar os dados corretamente', () => {
    component.dataSource.data = [
      { placa: 'AAA-1111' },
      { placa: 'BBB-2222' },
      { placa: 'CCC-3333' },
    ];

    const inputEvent = {
      target: { value: 'bbb' }
    } as unknown as Event;

    component.applyFilter(inputEvent);

    expect(component.dataSource.filteredData.length).toBe(1);
    expect(component.dataSource.filteredData[0].placa).toBe('BBB-2222');
  });

  it('deve ordenar os veículos por distância', () => {
    component.userLocation = { latitude: -25.4284, longitude: -49.2733 };

    const unordered = [
      { coordenadas: { latitude: -25.5, longitude: -49.3 } },
      { coordenadas: { latitude: -25.43, longitude: -49.27 } }
    ];

    const ordenados = component.sortByDistance(unordered);

    const menorDistancia = component.calculateDistance(
      component.userLocation,
      ordenados[0].coordenadas
    );

    const maiorDistancia = component.calculateDistance(
      component.userLocation,
      ordenados[1].coordenadas
    );

    expect(menorDistancia).toBeLessThan(maiorDistancia);
  });
});