import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AdminComponent } from './components/admin/admin.component';
import { LoginComponent } from './components/login/login.component';
import { ErrorComponent } from './components/error/error.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { CustomerComponent } from './components/customer/customer.component';
import { ViewAccountComponent } from './components/view-account/view-account.component';
import { AddAccountComponent } from './components/add-account/add-account.component';
import { AddFdComponent } from './components/add-fd/add-fd.component';
import { ViewFdComponent } from './components/view-fd/view-fd.component';
import { AuthGuard } from './components/authguard/auth.guard';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: RegistrationComponent },
  { path: 'admin/dashboard', component: AdminComponent, canActivate: [AuthGuard] }, // Apply AuthGuard here
  { path: 'customer/dashboard', component: CustomerComponent, canActivate: [AuthGuard] }, // Apply AuthGuard here
  { path: 'view/acount', component: ViewAccountComponent, canActivate: [AuthGuard] }, // Apply AuthGuard here
  { path: 'add/account', component: AddAccountComponent, canActivate: [AuthGuard] }, // Apply AuthGuard here
  { path: 'add/FD', component: AddFdComponent, canActivate: [AuthGuard] }, // Apply AuthGuard here
  { path: 'view/FD', component: ViewFdComponent, canActivate: [AuthGuard] }, // Apply AuthGuard here

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
