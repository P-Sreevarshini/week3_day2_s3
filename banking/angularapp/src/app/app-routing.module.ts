import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AdminComponent } from './components/admin/admin.component';
import { LoginComponent } from './components/login/login.component';
import { ErrorComponent } from './components/error/error.component';
import { RegistrationComponent } from './components/registration/registration.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: RegistrationComponent },
  { path: 'admin/dashboard', component: AdminComponent },
  {
    path: 'error',
    component: ErrorComponent,
    data: { message: 'Oops! Something went wrong.' },
  },
  { path: '**', redirectTo: '/error', pathMatch: 'full' }, // Handle all other routes and redirect to the error page
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
