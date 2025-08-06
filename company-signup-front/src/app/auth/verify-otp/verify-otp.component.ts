import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { FormGroup, FormBuilder, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from '../auth-routing.module';

@Component({
  selector: 'app-verify-otp',
  imports: [ CommonModule,
    FormsModule,
    RouterModule,

    ReactiveFormsModule,
    AuthRoutingModule],
  templateUrl: './verify-otp.component.html',
  styleUrl: './verify-otp.component.scss'
})
export class VerifyOtpComponent implements OnInit {

  otpForm: FormGroup;
  submitted = false;
  companyId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.otpForm = this.fb.group({
      otp: ['', [Validators.required, Validators.pattern(/^\d{6}$/)]]
    });
  }

  ngOnInit(): void {
    this.companyId = Number(localStorage.getItem('companyId'));
  }

  onSubmit() {
    this.submitted = true;
    if (this.otpForm.invalid || this.companyId == null) return;

    const payload = {
      companyId: this.companyId,
      otp: this.otpForm.value.otp
    };

    this.authService.verifyOtp(payload).subscribe({
      next: (res: any) => {
        alert(res.message);
        localStorage.removeItem('otp');
        this.router.navigate(['/set-password']);
      },
      error: (err) => {
        alert(err?.error?.message || "OTP verification failed");
      }
    });
  }
}
