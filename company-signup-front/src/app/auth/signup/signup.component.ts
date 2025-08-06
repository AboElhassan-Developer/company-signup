import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl,
  ValidationErrors,
  FormsModule,
  ReactiveFormsModule
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule
  ]
})
export class SignupComponent implements OnInit {
  signUpForm!: FormGroup;
  logoPreview: string | ArrayBuffer | null = null;
  logoFile: File | null = null;
  logoUrl: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.signUpForm = this.fb.group({
      arabicName: ['', [Validators.required, Validators.maxLength(100), this.arabicOnlyValidator]],
      englishName: ['', [Validators.required, Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [this.egyptianPhoneValidator]],
      websiteUrl: ['', [this.websiteValidator]],
      logoUrl: ['']
    });
  }


  egyptianPhoneValidator(control: AbstractControl): ValidationErrors | null {
    const value = control.value;
    if (!value) return null;
    const regex = /^01[0125][0-9]{8}$/;
    return regex.test(value) ? null : { invalidPhone: true };
  }


  websiteValidator(control: AbstractControl): ValidationErrors | null {
    const value = control.value;
    if (!value) return null;
    const isValid = /^(https?:\/\/)/.test(value);
    return isValid ? null : { invalidUrl: true };
  }


  onLogoSelected(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (!file) return;

    const allowedExtensions = ['.jpg', '.jpeg', '.png', '.gif'];
    const fileExt = file.name.substring(file.name.lastIndexOf('.')).toLowerCase();
    if (!allowedExtensions.includes(fileExt)) {
      alert("Invalid logo file type. Only jpg, jpeg, png, gif allowed.");
      return;
    }

    this.logoFile = file;
    const reader = new FileReader();
    reader.onload = () => (this.logoPreview = reader.result);
    reader.readAsDataURL(file);

    const formData = new FormData();
    formData.append('logo', this.logoFile);
    this.authService.uploadLogo(formData).subscribe({
      next: (res) => {
this.logoUrl = res.logoUrl;
        this.signUpForm.patchValue({ logoUrl: this.logoUrl });
      },
      error: (err) => {
        alert("Logo upload failed: " + err.message);
      }
    });
  }


  onSubmit() {
    if (!this.signUpForm.valid) {
      this.signUpForm.markAllAsTouched();
      return;
    }

    const payload = { ...this.signUpForm.value, logoUrl: this.logoUrl };

    this.authService.registerCompany(payload).subscribe({
      next: (res: any) => {
      alert(`OTP has been sent to ${payload.email}. Please check your inbox.`);
        localStorage.setItem('companyId', res.companyId);
        
        this.router.navigate(['/verify-otp']);
      },
      error: (err) => {
        alert(err?.error?.message || "Signup failed");
      }
    });
  }


  get f() {
    return this.signUpForm.controls;
  }

  arabicOnlyValidator(control: AbstractControl): ValidationErrors | null {
  const value = control.value;
  if (!value) return null;

  const arabicRegex = /^[\u0600-\u06FF\s]+$/;
  return arabicRegex.test(value) ? null : { notArabic: true };
}
}
