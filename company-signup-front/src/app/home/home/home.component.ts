import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthRoutingModule } from '../../auth/auth-routing.module';

@Component({
  selector: 'app-home',
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,

    ReactiveFormsModule,
    AuthRoutingModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
companyName: string = '';
  logoUrl: string = '';
constructor(private router: Router) {}

  ngOnInit(): void {
    const companyData = localStorage.getItem('companyData');
    if (companyData) {
      const parsedData = JSON.parse(companyData);
this.companyName = parsedData.name;
      this.logoUrl = parsedData.logoPath;
    } else {
      this.router.navigate(['/login']);
    }
  }

  logout(): void {
    localStorage.removeItem('companyData');
    this.router.navigate(['/login']);
  }
}


