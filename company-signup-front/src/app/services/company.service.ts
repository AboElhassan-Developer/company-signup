import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  private baseUrl = 'https://localhost:7053/api/company';

  constructor(private http: HttpClient) {}

  
}
