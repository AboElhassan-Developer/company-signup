import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { AuthRoutingModule } from '../auth-routing.module';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,

    ReactiveFormsModule,
    AuthRoutingModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginForm: FormGroup;
  submitted = false;
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

 onSubmit() {
  this.submitted = true;
  if (this.loginForm.invalid) return;

  this.http.post<any>('https://localhost:7053/api/Auth/login', this.loginForm.value)
    .subscribe({
      next: (res) => {
        alert(res.message);


        const companyData = {
          name: res.companyName,
          logoPath: res.logoPath
        };
        localStorage.setItem('companyData', JSON.stringify(companyData));


        this.router.navigate(['/home/home']);
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Login failed';
      }
    });
}

}
