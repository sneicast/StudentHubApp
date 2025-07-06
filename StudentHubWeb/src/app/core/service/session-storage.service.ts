import { Injectable } from '@angular/core';
import { StudentInfo } from '../dtos/student-info';

@Injectable({
  providedIn: 'root'
})
export class SessionStorageService {
private readonly TOKEN_KEY = 'accessToken';
  private readonly STUDENT_INFO_KEY = 'studentInfo';

  constructor() { }

   // ==================== TOKEN METHODS ====================
 
  setToken(token: string): void {
    sessionStorage.setItem(this.TOKEN_KEY, token);
  }

  getToken(): string | null {
    try {
      return sessionStorage.getItem(this.TOKEN_KEY);
    } catch (error) {
      return null;
    }
  }

  removeToken(): void {
    sessionStorage.removeItem(this.TOKEN_KEY);
  }

   // ==================== STUDENT INFO METHODS ====================
 
  setStudentInfo(info: StudentInfo): void {
    sessionStorage.setItem(this.STUDENT_INFO_KEY, JSON.stringify(info));
  }

  getStudentInfo(): StudentInfo | null {
    const info = sessionStorage.getItem(this.STUDENT_INFO_KEY);
    return info ? JSON.parse(info) : null;
  }

  removeStudentInfo(): void {
    sessionStorage.removeItem(this.STUDENT_INFO_KEY);
  }

  // ==================== UTILITY METHODS ====================
  
   clearSession(): void {
    this.removeToken();
    this.removeStudentInfo();
  }
  
  hasValidSession(): boolean {
    return this.getToken() !== null;
  }
}
