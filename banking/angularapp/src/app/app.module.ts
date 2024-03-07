import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminComponent } from './components/admin/admin.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RegistrationComponent } from './components/registration/registration.component';
import { LoginComponent } from './components/login/login.component';
import { AddFdComponent } from './components/add-fd/add-fd.component';
import { AddReviewComponent } from './components/add-review/add-review.component';
import { AddAccountComponent } from './components/add-account/add-account.component';
import { ViewAccountComponent } from './components/view-account/view-account.component';
import { CustomerComponent } from './components/customer/customer.component';
import { ViewReviewComponent } from './components/view-review/view-review.component';
import { ViewFdComponent } from './components/view-fd/view-fd.component';
import { AddFdaccountComponent } from './components/add-fdaccount/add-fdaccount.component';
import { ViewFdaccountComponent } from './components/view-fdaccount/view-fdaccount.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    NavbarComponent,
    RegistrationComponent,
    LoginComponent,
    AddFdComponent,
    ViewFdComponent,
    AddReviewComponent,
    ViewReviewComponent,
    AddAccountComponent,
    ViewAccountComponent,
    CustomerComponent,
    AddFdaccountComponent,
    ViewFdaccountComponent
    ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
