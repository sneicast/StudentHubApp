import { Component, OnInit } from '@angular/core';
import { StudentClassesService } from '../service/student-classes-service';
import { StudentClassResponseDto } from '../dtos/student-class-response.dto';
import { ClassDto } from '../dtos/class.dto';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { StudentDto } from '../../../core/dtos/student.dto';
import { StudentInfo } from '../../../core/dtos/student-info';
import { SessionStorageService } from '../../../core/service/session-storage.service';
import { ErrorHandlerUtil } from '../../../core/utils/error-handler.util';

@Component({
  selector: 'app-student-classes-page',
  imports: [CommonModule, FormsModule],
  templateUrl: './student-classes-page.html',
  styleUrl: './student-classes-page.css'
})
export class StudentClassesPage implements OnInit {

  ListAvailableClasses: ClassDto[] = [];
  ListStudentClasses: StudentClassResponseDto[] = [];
  ClassStudents: StudentDto[] = [];

  isLoading = false;
  showEnrollModal = false;
  showStudentsModal = false;
  isEnrolling = false;
  isLoadingAvailableClasses = false;
  isLoadingStudents = false;

  selectedClassId: number | null = null;

  selectedClassName = '';

   studentInfo: StudentInfo | null = null;

  constructor(private studentClassesService: StudentClassesService,private sessionStoreService: SessionStorageService ) { }

  ngOnInit(): void {
    this.getStudentClasses();
     this.loadStudentInfo();
  }


 

  removeClass(classId: number, className: string): void {
    if (confirm(`¿Estás seguro de que quieres eliminar la clase "${className}"?`)) {
      this.isLoading = true;
      this.studentClassesService.removeStudentFromClass(classId).subscribe({
        next: () => {
          this.getStudentClasses();
          this.getAvailableClasses();
          console.log(`Clase "${className}" eliminada exitosamente`);
        },
        error: (error) => {
          console.error('Error removing class:', error);
          this.isLoading = false;
        }
      });
    }
  }

  openEnrollModal(): void {
    this.getAvailableClasses();
    this.showEnrollModal = true;
    this.selectedClassId = null;
  }

  closeEnrollModal(): void {
    this.showEnrollModal = false;
    this.selectedClassId = null;
  }

  selectClass(classId: number): void {
    this.selectedClassId = classId;
  }



  enrollInClass(): void {
    if (!this.selectedClassId) {
      alert('Por favor selecciona una clase');
      return;
    }
    this.isEnrolling = true;

    this.studentClassesService.enrollInClass({ classId: this.selectedClassId }).subscribe({
      next: (response) => {

        this.getStudentClasses();
        this.closeEnrollModal();
        this.isEnrolling = false;
      },
      error: (error) => {
        this.isEnrolling = false;
        alert(ErrorHandlerUtil.handleError(error.error, 'Error al inscribirse en la clase. Por favor intenta de nuevo.'));
      }
    });

  }

  
  openStudentsModal(classId: number, className: string): void {
    this.selectedClassId = classId;
    this.selectedClassName = className;
    this.showStudentsModal = true;
    this.getClassStudents(classId);
  }
  closeStudentsModal(): void {
    this.showStudentsModal = false;
    this.selectedClassId = null;
    this.selectedClassName = '';
    this.ClassStudents = [];
  }

  getClassStudents(classId: number): void {
    this.isLoadingStudents = true;
    this.studentClassesService.getStudentsInClass(classId).subscribe({
      next: (students) => {
        this.ClassStudents = students;
        this.isLoadingStudents = false;
      },
      error: (error) => {
        this.isLoadingStudents = false;
        alert(ErrorHandlerUtil.handleError(error.error, 'Error al cargar los estudiantes de la clase.'));
      }
    });
  }

  // Método helper para obtener el nombre completo
  getFullName(student: StudentDto): string {
    if (student.surnames) {
      return `${student.name} ${student.surnames}`;
    }
    return student.name;
  }





   getAvailableClasses(): void {
    this.isLoadingAvailableClasses = true;
    this.studentClassesService.getAvailableClasses().subscribe({
      next: (classes) => {
        this.ListAvailableClasses = classes;
        this.isLoadingAvailableClasses = false;
      },
      error: (error) => {
        this.isLoadingAvailableClasses = false;
        alert(ErrorHandlerUtil.handleError(error.error, 'Error al cargar las clases disponibles.'));
      }
    });
  }

  getStudentClasses(): void {
    this.isLoading = true;
    this.studentClassesService.getStudentClasses().subscribe({
      next: (classes) => {
        this.ListStudentClasses = classes;
        this.isLoading = false;
      },
      error: (error) => {
        this.isLoading = false;
        alert(ErrorHandlerUtil.handleError(error.error, 'Error al cargar las clases del estudiante.'));
      }
    });
  }



   loadStudentInfo(): void {
    try {
      const storedInfo = sessionStorage.getItem('studentInfo');
      if (storedInfo) {
        this.studentInfo = JSON.parse(storedInfo);
        console.log('Student info loaded:', this.studentInfo);
      }
    } catch (error) {
      console.error('Error loading student info from session:', error);
    }
  }

  // Método para calcular los créditos actuales (suma de créditos de clases inscritas)
  getCurrentCredits(): number {
    if (!this.ListStudentClasses || this.ListStudentClasses.length === 0) {
      return 0;
    }
    return this.ListStudentClasses.reduce((total, studentClass) => {
      return total + (studentClass.credits || 0);
    }, 0);
  }

  // Método para calcular el porcentaje de progreso
  getProgressPercentage(): number {
    if (!this.studentInfo?.totalCredits || this.studentInfo.totalCredits === 0) {
      return 0;
    }
    const current = this.getCurrentCredits();
    const percentage = (current / this.studentInfo.totalCredits) * 100;
    return Math.min(percentage, 100); // No exceder el 100%
  }

  // Método para obtener el color del progreso basado en el porcentaje
  getProgressColor(): string {
    const percentage = this.getProgressPercentage();
    if (percentage < 30) return 'bg-red-500';
    if (percentage < 70) return 'bg-yellow-500';
    return 'bg-green-500';
  }

  // Método para obtener el color del texto basado en el porcentaje
  getProgressTextColor(): string {
    const percentage = this.getProgressPercentage();
    if (percentage < 30) return 'text-red-600';
    if (percentage < 70) return 'text-yellow-600';
    return 'text-green-600';
  }

  // Método para obtener mensaje motivacional
  getProgressMessage(): string {
    const percentage = this.getProgressPercentage();
    if (percentage === 0) return '¡Comienza tu journey académico!';
    if (percentage < 25) return '¡Buen comienzo! Sigue adelante';
    if (percentage < 50) return '¡Vas por buen camino!';
    if (percentage < 75) return '¡Excelente progreso!';
    if (percentage < 100) return '¡Casi terminas!';
    return '¡Felicidades! Has completado todos los créditos';
  }



}
