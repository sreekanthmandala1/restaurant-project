import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MenuListComponent } from './components/menu-list/menu-list.component';
import { MenuFormComponent } from './components/menu-form/menu-form.component';

const routes: Routes = [
  { path: 'menu', component: MenuListComponent },
  { path: 'menu/add', component: MenuFormComponent },
  { path: '**', redirectTo: 'menu/add' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
