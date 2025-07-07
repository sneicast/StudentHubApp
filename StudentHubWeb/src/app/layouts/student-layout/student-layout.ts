import { Component, HostListener, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { SessionStorageService } from '../../core/service/session-storage.service';
import { StudentInfo } from '../../core/dtos/student-info';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-student-layout',
  imports: [CommonModule, RouterOutlet, RouterLink],
  templateUrl: './student-layout.html',
  styleUrl: './student-layout.css'
})
export class StudentLayout implements OnInit {
  studentInfo: StudentInfo | null = null;
  isSidebarOpen = false;
  isMobile = false;
  menuItems = [
    {
      name: 'Mis Clases',
      icon: 'academic-cap',
      route: '/student/classes',
      active: false
    }
  ];

  constructor(private sessionStorageService: SessionStorageService, private router: Router) { }

  ngOnInit(): void {
    this.loadStudentInfo();
    this.checkScreenSize();
  }

  private loadStudentInfo(): void {
    this.studentInfo = this.sessionStorageService.getStudentInfo();

    if (!this.studentInfo) {
      this.router.navigate(['/login']);
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any): void {
    this.checkScreenSize();
  }

  private checkScreenSize(): void {
    this.isMobile = window.innerWidth < 1024;
    
    if (!this.isMobile) {
      this.isSidebarOpen = false;
    }
  }
  toggleSidebar(): void {
    this.isSidebarOpen = !this.isSidebarOpen;
  }

  logout(): void {
    this.sessionStorageService.clearSession();
    this.router.navigate(['/login']);
  }

  setActiveMenuItem(route: string): void {
    this.menuItems.forEach(item => {
      item.active = item.route === route;
    });
    if (this.isMobile) {
      setTimeout(() => {
        this.isSidebarOpen = false;
      }, 100);
    }
  }

  getIconPath(iconName: string): string {
    const icons: { [key: string]: string } = {
      'home': 'M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6',
      'academic-cap': 'M12 14l9-5-9-5-9 5 9 5zm0 0l6.16-3.422a12.083 12.083 0 01.665 6.479A11.952 11.952 0 0012 20.055a11.952 11.952 0 00-6.824-2.998 12.078 12.078 0 01.665-6.479L12 14zm-4 6v-7.5l4-2.222',
      'calendar': 'M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z',
      'chart-bar': 'M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z',
      'user': 'M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z',
      'menu': 'M4 6h16M4 12h16M4 18h16',
      'x': 'M6 18L18 6M6 6l12 12',
      'logout': 'M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1'
    };

    return icons[iconName] || icons['home'];
  }

}
