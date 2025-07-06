import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreditProgramDto } from '../dtos/credit-program.dto';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CreditProgramService {
  private readonly URL_BASE = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCreditPrograms(): Observable<CreditProgramDto[]> {
    return this.http.get<CreditProgramDto[]>(`${this.URL_BASE}/api/CreditProgram`);
  }
}
