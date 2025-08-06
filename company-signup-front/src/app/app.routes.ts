import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { SetPasswordComponent } from './auth/set-password/set-password.component';
import { SignupComponent } from './auth/signup/signup.component';
import { VerifyOtpComponent } from './auth/verify-otp/verify-otp.component';
import { HomeComponent } from './home/home/home.component';

export const routes: Routes = [

{ path: '', redirectTo: 'signup', pathMatch: 'full' },
  { path: 'signup', component: SignupComponent },
  { path: 'verify-otp', component: VerifyOtpComponent },
  { path: 'set-password', component: SetPasswordComponent },
  { path: 'login', component: LoginComponent },
  { path: 'home/home', component: HomeComponent },
{ path: '**', redirectTo: 'signup' }
];
