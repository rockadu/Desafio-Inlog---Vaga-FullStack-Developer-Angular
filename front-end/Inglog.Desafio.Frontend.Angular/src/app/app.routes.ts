import { Routes } from '@angular/router';
import { VeiculosListar } from './pages/veiculos-listar/veiculos-listar';
import { VeiculosCadastrar } from './pages/veiculos-cadastrar/veiculos-cadastrar';

export const routes: Routes = [
  { path: '', redirectTo: 'listar', pathMatch: 'full' },
  { path: 'listar', component: VeiculosListar },
  { path: 'cadastrar', component: VeiculosCadastrar }
];