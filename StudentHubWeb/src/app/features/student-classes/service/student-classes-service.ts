import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StudentClassRequestDto } from '../dtos/student-class-request.dto';
import { Observable } from 'rxjs';
import { ClassDto } from '../dtos/class.dto';
import { StudentClassResponseDto } from '../dtos/student-class-response.dto';
import { StudentDto } from '../../../core/dtos/student.dto';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StudentClassesService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAvailableClasses(): Observable<ClassDto[]> {
    return this.http.get<ClassDto[]>(`${this.apiUrl}/api/Classes/available`);
  }
  getStudentClasses(): Observable<StudentClassResponseDto[]> {
    return this.http.get<StudentClassResponseDto[]>(`${this.apiUrl}/api/Classes/student`);
  }
  
  enrollInClass(request: StudentClassRequestDto): Observable<StudentClassResponseDto> {
    return this.http.post<StudentClassResponseDto>(`${this.apiUrl}/api/Classes/student/enroll`, request);
  }

   removeStudentFromClass(classId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/api/Classes/${classId}/unenroll`);
  }

  getStudentsInClass(classId: number): Observable<StudentDto[]> {
    return this.http.get<StudentDto[]>(`${this.apiUrl}/api/Classes/${classId}/students`);
  }
}
