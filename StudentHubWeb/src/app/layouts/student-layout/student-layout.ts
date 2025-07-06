import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SessionStorageService } from '../../core/service/session-storage.service';
import { StudentInfo } from '../../core/dtos/student-info';

@Component({
  selector: 'app-student-layout',
  imports: [RouterOutlet],
  templateUrl: './student-layout.html',
  styleUrl: './student-layout.css'
})
export class StudentLayout implements OnInit {
  studentInfo: StudentInfo | null = null;
  constructor(private sessionStorageService: SessionStorageService) { }
  ngOnInit(): void {
    this.studentInfo = this.sessionStorageService.getStudentInfo();
  }

}
