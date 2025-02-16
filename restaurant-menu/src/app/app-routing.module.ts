import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MenuListComponent } from './components/menu-list/menu-list.component';
import { MenuFormComponent } from './components/menu-form/menu-form.component';
import { authGuard } from './auth/auth.guard';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'menu', component: MenuListComponent },
  { path: 'menu/add', component: MenuFormComponent },
  { path: 'admin', component: MenuFormComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
