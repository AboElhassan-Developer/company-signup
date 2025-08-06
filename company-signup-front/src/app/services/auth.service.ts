import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'https://localhost:7053/api/auth';

   constructor(private http: HttpClient) {}

uploadLogo(data: FormData) {
  return this.http.post<{ logoUrl: string }>('https://localhost:7053/api/logo/upload', data);
}



registerCompany(model: any) {
  return this.http.post(`${this.baseUrl}/signup`, model);
}
verifyOtp(model: { companyId: number, otp: string }) {
return this.http.post(`${this.baseUrl}/verify-otp`, model);
}
setPassword(data: { companyId: number, password: string }) {
  return this.http.post(`${this.baseUrl}/set-password`, data);
}

  login(data: { email: string, password: string }) {
    return this.http.post(`${this.baseUrl}/login`, data);
  }
  checkEmailExists(email: string) {
  return this.http.get<boolean>(`${this.baseUrl}/check-email?email=${email}`);
}


}
