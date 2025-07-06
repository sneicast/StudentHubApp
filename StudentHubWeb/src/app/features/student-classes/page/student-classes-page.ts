import { Component, OnInit } from '@angular/core';
import { StudentClassesService } from '../service/student-classes-service';
import { StudentClassResponseDto } from '../dtos/student-class-response.dto';
import { ClassDto } from '../dtos/class.dto';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-student-classes-page',
  imports: [CommonModule, FormsModule],
  templateUrl: './student-classes-page.html',
  styleUrl: './student-classes-page.css'
})
export class StudentClassesPage implements OnInit {

  ListAvailableClasses: ClassDto[] = [];
  ListStudentClasses: StudentClassResponseDto[] = [];

  isLoading = false;
  showEnrollModal = false;
  isEnrolling = false;
  isLoadingAvailableClasses = false;
  selectedClassId: number | null = null;

  constructor(private studentClassesService: StudentClassesService) { }

  ngOnInit(): void {
    this.getStudentClasses();
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
        alert('Error al inscribirse en la clase. Por favor intenta de nuevo.');
      }
    });

  }


   getAvailableClasses(): void {
    this.isLoadingAvailableClasses = true;
    this.studentClassesService.getAvailableClasses().subscribe({
      next: (classes) => {
        this.ListAvailableClasses = classes;
        console.log('Available classes fetched successfully:', this.ListAvailableClasses);
        this.isLoadingAvailableClasses = false;
      },
      error: (error) => {
        console.error('Error fetching available classes:', error);
        this.isLoadingAvailableClasses = false;
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
        console.error('Error fetching student classes:', error);
        this.isLoading = false;
      }
    });
  }

  getIconPath(iconName: string): string {
    const icons: { [key: string]: string } = {
      'trash': 'M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16',
      'book': 'M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.747 0 3.332.477 4.5 1.253v13C19.832 18.477 18.246 18 16.5 18c-1.746 0-3.332.477-4.5 1.253',
      'academic-cap': 'M12 14l9-5-9-5-9 5 9 5zm0 0l6.16-3.422a12.083 12.083 0 01.665 6.479A11.952 11.952 0 0012 20.055a11.952 11.952 0 00-6.824-2.998 12.078 12.078 0 01.665-6.479L12 14zm-4 6v-7.5l4-2.222',
      'plus': 'M12 6v6m0 0v6m0-6h6m-6 0H6'
    };

    return icons[iconName] || icons['book'];
  }

}
