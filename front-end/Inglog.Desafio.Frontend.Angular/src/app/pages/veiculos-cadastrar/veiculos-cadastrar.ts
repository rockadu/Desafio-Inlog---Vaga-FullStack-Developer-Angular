import { Component, AfterViewInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { VeiculosService } from '../../services/veiculos';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import * as L from 'leaflet';

@Component({
  selector: 'app-veiculos-cadastrar',
  standalone: true,
  templateUrl: './veiculos-cadastrar.html',
  imports: [CommonModule, ReactiveFormsModule],
})

export class VeiculosCadastrar implements AfterViewInit {
  map!: L.Map;
  marker?: L.Marker;
  form: any;

  ngAfterViewInit(): void {
    this.initMap();
  }

  initMap(): void {
    this.map = L.map('mapa-cadastro').setView([-25.45, -49.25], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '© OpenStreetMap'
    }).addTo(this.map);

    this.map.on('click', (e: L.LeafletMouseEvent) => {
      const { lat, lng } = e.latlng;

      // Atualiza o form
      this.form.patchValue({
        latitude: lat,
        longitude: lng
      });

      // Mostra marcador
      if (this.marker) {
        this.marker.setLatLng([lat, lng]);
      } else {
        this.marker = L.marker([lat, lng]).addTo(this.map);
      }
    });
  }

  constructor(
    private fb: FormBuilder,
    private veiculosService: VeiculosService,
    private router: Router
  ) {
    this.form = this.fb.group({
      chassi: [veiculosService.gerarChassiAleatorio(), Validators.required],
      tipoVeiculo: [1, Validators.required],
      cor: ['#000000', Validators.required],
      placa: [veiculosService.gerarPlacaAleatoria(), [Validators.required, Validators.pattern(/^[A-Z]{3}-[0-9][A-Z0-9][0-9]{2}$/)]],
      rastreador: [veiculosService.gerarRastreadorAleatorio(), Validators.required],
      latitude: ['', Validators.required],
      longitude: ['', Validators.required],
    });

  }

  async submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const valores = this.form.value;

    const veiculo = {
      chassi: valores.chassi,
      tipoVeiculo: valores.tipoVeiculo,
      cor: valores.cor,
      placa: valores.placa,
      rastreador: valores.rastreador,
      coordenadas: {
        latitude: parseFloat(valores.latitude),
        longitude: parseFloat(valores.longitude),
      },
    };

    try {
      await this.veiculosService.cadastrar(veiculo);
      this.router.navigate(['/listar']);
    } catch (error) {
      console.error('Erro ao cadastrar veículo:', error);
      alert('Erro ao cadastrar veículo');
    }
  }

}
