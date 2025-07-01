import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { VeiculosService } from './veiculos';

describe('VeiculosService', () => {
  let service: VeiculosService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [VeiculosService]
    });

    service = TestBed.inject(VeiculosService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Verifica se não ficou nenhuma requisição pendente
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call listar and return an array', async () => {
    const mockResponse = [{ chassi: 'ABC123', placa: 'AAA-1A23' }];
    const listarPromise = service.listar();

    const req = httpMock.expectOne('https://localhost:7222/Veiculo/Listar');
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);

    const result = await listarPromise;
    expect(result).toEqual(mockResponse);
  });
});
