import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class VeiculosService {
  private readonly baseUrl = 'https://localhost:7222/Veiculo';

  constructor(private http: HttpClient) { }

  async cadastrar(veiculo: any): Promise<void> {
    await firstValueFrom(this.http.post(`${this.baseUrl}/Cadastrar`, veiculo));
  }

  gerarChassiAleatorio(): string {
    const caracteres = 'ABCDEFGHJKLMNPRSTUVWXYZ0123456789'; // sem I, O, Q
    let vin = '';
    for (let i = 0; i < 17; i++) {
      vin += caracteres[Math.floor(Math.random() * caracteres.length)];
    }
    return vin;
  }

  gerarPlacaAleatoria(): string {
    const letras = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
    const numeros = '0123456789';

    const parte1 = letras[Math.floor(Math.random() * letras.length)] +
      letras[Math.floor(Math.random() * letras.length)] +
      letras[Math.floor(Math.random() * letras.length)] + '-';

    const parte2 = numeros[Math.floor(Math.random() * numeros.length)];

    const parte3 = letras[Math.floor(Math.random() * letras.length)];

    const parte4 = numeros[Math.floor(Math.random() * numeros.length)] +
      numeros[Math.floor(Math.random() * numeros.length)];

    return `${parte1}${parte2}${parte3}${parte4}`;
  }

  gerarRastreadorAleatorio(): string {
    const letras = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
    const numeros = '0123456789';

    const parte1 = letras[Math.floor(Math.random() * letras.length)] +
      letras[Math.floor(Math.random() * letras.length)];

    const parte2 = numeros[Math.floor(Math.random() * numeros.length)] +
      numeros[Math.floor(Math.random() * numeros.length)] +
      numeros[Math.floor(Math.random() * numeros.length)];

    const parte3 = letras[Math.floor(Math.random() * letras.length)];

    return `${parte1}${parte2}${parte3}`;
  }
}