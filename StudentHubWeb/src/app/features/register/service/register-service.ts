import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { RegisterRequestDto } from '../dtos/register-request.dto';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private readonly URL_BASE = environment.apiUrl;

  constructor(private http: HttpClient) { }

  register(registerRequest: RegisterRequestDto) {
    return this.http.post(`${this.URL_BASE}/api/Student/register`, registerRequest);
  }
}
