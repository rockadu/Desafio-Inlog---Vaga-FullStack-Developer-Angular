import { Component, AfterViewInit } from '@angular/core';
import * as L from 'leaflet';
import { VeiculosService } from '../../services/veiculos';

@Component({
  selector: 'app-veiculos-listar',
  standalone: true,
  imports: [],
  templateUrl: './veiculos-listar.html',
})
export class VeiculosListar implements AfterViewInit {
  map!: L.Map;
  vehicles: any[] = [];
  userLocation?: { latitude: number; longitude: number };

  constructor(private veiculosService: VeiculosService) {}

  async ngAfterViewInit() {
    this.getUserLocation();
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
      async () => {
        this.userLocation = { latitude: -25.4284, longitude: -49.2733 }; // Curitiba fallback
        await this.loadVehiclesAndMap();
      }
    );
  }

  async loadVehiclesAndMap() {
    this.initializeMap();

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
        L.marker([coords.latitude, coords.longitude])
          .addTo(this.map)
          .bindPopup(`Placa: ${vehicle.placa}<br>Cor: ${vehicle.cor}`);
      }
    });
  }

  sortByDistance(vehicles: any[]): any[] {
    return vehicles.sort((a, b) => {
      const distA = this.calculateDistance(this.userLocation!, a.coordenadas);
      const distB = this.calculateDistance(this.userLocation!, b.coordenadas);
      return distA - distB;
    });
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
}
