import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { SessionStorageService } from '../../../core/service/session-storage.service';

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  accessToken: string;
  fullName: string;
  email: string;
  creditProgramId: number;
  creditProgramName: string;
  totalCredits: number;
}

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private readonly TOKEN_KEY = 'accessToken';

  constructor(private http: HttpClient, private sessionStorageService: SessionStorageService) { }

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.apiUrl}/api/Student/login`, credentials).pipe(
      tap(response => {
        sessionStorage.setItem(this.TOKEN_KEY, response.accessToken);
         this.sessionStorageService.setToken(response.accessToken);
          this.sessionStorageService.setStudentInfo({
          fullName: response.fullName,
          email: response.email,
          creditProgramId: response.creditProgramId,
          creditProgramName: response.creditProgramName,
          totalCredits: response.totalCredits,
        });
      })
    );
  }

  logout(): void {
     this.sessionStorageService.clearSession();
  }

  getToken(): string | null {
    return this.sessionStorageService.getToken();
  }

  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }

}
