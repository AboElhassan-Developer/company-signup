import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from '../auth-routing.module';

@Component({
  selector: 'app-set-password',
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    AuthRoutingModule
  ],
  templateUrl: './set-password.component.html',
  styleUrl: './set-password.component.scss'
})
export class SetPasswordComponent implements OnInit {
  setPasswordForm: FormGroup;
  companyId: number | null = null;

  passwordChecks = {
    minLength: false,
    upperCase: false,
    number: false,
    specialChar: false
  };

  passwordTouched = false;
  confirmTouched = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.setPasswordForm = this.fb.group({
      password: ['', [Validators.required]],
      confirmPassword: ['', Validators.required]
    }, { validators: this.passwordMatchValidator });

   
    this.setPasswordForm.get('password')?.valueChanges.subscribe(value => {
      this.passwordChecks.minLength = value?.length >= 6;
      this.passwordChecks.upperCase = /[A-Z]/.test(value);
      this.passwordChecks.number = /\d/.test(value);
      this.passwordChecks.specialChar = /[@$!%*?&]/.test(value);
    });
  }

  get f() {
    return this.setPasswordForm.controls;
  }

  ngOnInit(): void {
    this.companyId = Number(localStorage.getItem('companyId'));
  }

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  onSubmit(): void {
    this.passwordTouched = true;
    this.confirmTouched = true;

    if (this.setPasswordForm.invalid || this.companyId == null) return;

    const payload = {
      companyId: this.companyId,
      password: this.setPasswordForm.value.password,
      confirmPassword: this.setPasswordForm.value.confirmPassword
    };

    this.authService.setPassword(payload).subscribe({
      next: (res: any) => {
        alert(res.message || "Password set successfully");
        localStorage.removeItem('companyId');
        localStorage.removeItem('otp');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        alert(err?.error?.message || "Setting password failed");
      }
    });
  }
}
