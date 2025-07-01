import { Component, ViewChild, AfterViewInit } from '@angular/core';
import * as L from 'leaflet';
import { VeiculosService } from '../../services/veiculos';
import { CommonModule } from '@angular/common';
import { MatTableDataSource } from '@angular/material/table';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-veiculos-listar',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './veiculos-listar.html',
})

export class VeiculosListar implements AfterViewInit {
  map!: L.Map;
  vehicles: any[] = [];
  userLocation?: { latitude: number; longitude: number };
  displayedColumns: string[] = ['chassi', 'placa', 'cor', 'tipoVeiculo', 'distanciaMetros', 'acoes'];
  dataSource = new MatTableDataSource<any>([]);
  selectedChassi?: string;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private veiculosService: VeiculosService) { }

  async ngAfterViewInit() {
    this.getUserLocation();

    const veiculos = await this.veiculosService.listar();
    const veiculosComDistancia = this.sortByDistance(veiculos);

    this.vehicles = veiculosComDistancia;
    this.dataSource.data = veiculosComDistancia;
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    this.dataSource.sortingDataAccessor = (item, property) => {
      switch (property) {
        case 'distanciaMetros':
          return item.distanciaMetros;
        default:
          return item[property];
      }
    };

    setTimeout(() => {
      this.sort.active = 'distanciaMetros';
      this.sort.direction = 'asc';
      this.sort.sortChange.emit();
    });
  }

  getUserLocation() {
    navigator.geolocation.getCurrentPosition(
      async (position) => {
        this.userLocation = {
          latitude: position.coords.latitude,
          longitude: position.coords.longitude,
        };
        await this.loadVehiclesAndMap();
      },
      async (error) => {
        this.userLocation = { latitude: -25.4284, longitude: -49.2733 };
        await this.loadVehiclesAndMap();
      },
      {
        enableHighAccuracy: true,
        timeout: 5000,
        maximumAge: 0
      }
    );
  }

  irParaVeiculo(veiculo: any) {
    if (this.selectedChassi === veiculo.chassi) {
      this.selectedChassi = undefined;
      this.map.setView([this.userLocation!.latitude, this.userLocation!.longitude], 13);
    } else {
      this.selectedChassi = veiculo.chassi;
      const coords = veiculo.coordenadas;
      if (coords?.latitude && coords?.longitude) {
        this.map.setView([coords.latitude, coords.longitude], 16);
      }
    }
  }

  refreshMap() {
    this.map.eachLayer((layer) => {
      if ((layer as any)._icon) this.map.removeLayer(layer);
    });

    this.addMarkersToMap(this.vehicles);
  }

  async deletarVeiculo(chassi: string) {
    if (!confirm(`Tem certeza que deseja deletar o veículo com chassi ${chassi}?`)) return;

    try {
      await this.veiculosService.deletar(chassi);
      this.vehicles = this.vehicles.filter(v => v.chassi !== chassi);
      this.refreshMap();
    } catch (error) {
      alert('Erro ao deletar veículo');
    }
  }

  async loadVehiclesAndMap() {
    this.initializeMap();
    L.marker([this.userLocation!.latitude, this.userLocation!.longitude], {
      icon: L.icon({
        iconUrl: 'https://cdn-icons-png.flaticon.com/512/684/684908.png',
        iconSize: [32, 32],
        iconAnchor: [16, 32],
      }),
    })
      .addTo(this.map)
      .bindPopup('Você está aqui');

    try {
      const response = await this.veiculosService.listar();
      this.vehicles = this.sortByDistance(response);
      this.addMarkersToMap(this.vehicles);
    } catch (error) {
      console.error('Erro ao listar veículos:', error);
    }
  }

  initializeMap() {
    this.map = L.map('mapa').setView(
      [this.userLocation!.latitude, this.userLocation!.longitude],
      13
    );

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '© OpenStreetMap contributors',
    }).addTo(this.map);
  }

  addMarkersToMap(vehicles: any[]) {
    vehicles.forEach((vehicle) => {
      const coords = vehicle.coordenadas;
      if (coords?.latitude && coords?.longitude) {
        L.marker([coords.latitude, coords.longitude], {
          icon: L.divIcon({
            className: 'text-2xl', // Tailwind opcional para tamanho
            html: `
                <div style="
                  text-shadow: 1px 1px 2px black;
                ">
                  ${vehicle.tipoVeiculo === 2 ? '🚛' : '🚌'}
                </div>`,
            iconSize: [30, 30],
            iconAnchor: [15, 15]
          })
        }).addTo(this.map)
          .bindPopup(`
            Placa: ${vehicle.placa}<br>
            Cor: <span class="inline-block w-4 h-4 rounded-full border" style="background-color: ${vehicle.cor};"></span><br>
            Distância: ${vehicle.distanciaMetros ? vehicle.distanciaMetros.toFixed(0) + ' m' : 'Desconhecida'}<br>
            Chassi: ${vehicle.chassi}
          `);
      }
    });
  }

  sortByDistance(vehicles: any[]): any[] {
    return vehicles
      .map((vehicle) => {
        const distance = this.calculateDistance(this.userLocation!, {
          latitude: vehicle.coordenadas?.latitude,
          longitude: vehicle.coordenadas?.longitude,
        });

        return { ...vehicle, distanciaMetros: distance };
      })
      .sort((a, b) => a.distanciaMetros - b.distanciaMetros);
  }

  calculateDistance(
    origin: { latitude: number; longitude: number },
    destination: { latitude: number; longitude: number }
  ): number {
    if (!destination) return Infinity;

    const earthRadius = 6371000;
    const toRadians = (deg: number) => deg * (Math.PI / 180);

    const dLat = toRadians(destination.latitude - origin.latitude);
    const dLon = toRadians(destination.longitude - origin.longitude);
    const lat1 = toRadians(origin.latitude);
    const lat2 = toRadians(destination.latitude);

    const a =
      Math.sin(dLat / 2) ** 2 +
      Math.cos(lat1) * Math.cos(lat2) * Math.sin(dLon / 2) ** 2;

    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    return earthRadius * c;
  }

  applyFilter(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    this.dataSource.filter = value.trim().toLowerCase();

    // Espera o Angular aplicar o filtro
    setTimeout(() => {
      const filtrados = this.dataSource.filteredData;
      this.refreshMapWith(filtrados);
    });
  }

  refreshMapWith(vehicles: any[]) {
    if (!this.map) return;

    // Remove todos os marcadores do mapa
    this.map.eachLayer((layer) => {
      if ((layer as any)._icon || (layer as any) instanceof L.Marker || (layer as any) instanceof L.CircleMarker) {
        this.map.removeLayer(layer);
      }
    });

    const bounds: L.LatLngTuple[] = [];

    const isFiltroAtivo = vehicles.length !== this.vehicles.length;

    if (this.userLocation && !isFiltroAtivo) {
      const userMarker = L.circleMarker(
        [this.userLocation.latitude, this.userLocation.longitude],
        {
          radius: 6,
          color: '#000',
          fillColor: '#000',
          fillOpacity: 1,
        }
      ).bindPopup('Você está aqui');
      userMarker.addTo(this.map);
      bounds.push([this.userLocation.latitude, this.userLocation.longitude]);
    }

    // Adiciona veículos
    vehicles.forEach((vehicle) => {
      const coords = vehicle.coordenadas;
      if (coords?.latitude && coords?.longitude) {
        const marker = L.marker([coords.latitude, coords.longitude], {
          icon: L.divIcon({
            className: 'text-2xl',
            html: `
            <div style="text-shadow: 1px 1px 2px black;">
              ${vehicle.tipoVeiculo === 2 ? '🚛' : '🚌'}
            </div>`,
            iconSize: [30, 30],
            iconAnchor: [15, 15]
          })
        }).bindPopup(`Placa: ${vehicle.placa}<br>Cor: ${vehicle.cor}`);
        marker.addTo(this.map);

        bounds.push([coords.latitude, coords.longitude]);
      }
    });

    // Ajuste do zoom
    if (bounds.length > 1) {
      this.map.fitBounds(bounds, { padding: [30, 30] });
    } else if (bounds.length === 1) {
      this.map.setView(bounds[0], 15);
    }
  }
}
