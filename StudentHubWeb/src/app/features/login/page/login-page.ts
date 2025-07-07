import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from '../service/login-service';
import { ErrorHandlerUtil } from '../../../core/utils/error-handler.util';

@Component({
  selector: 'app-login-page',
  imports: [FormsModule],
  templateUrl: './login-page.html',
  styleUrl: './login-page.css'
})
export class LoginPage {
  email: string = '';
  password: string = '';
  isLoading: boolean = false;
  errorMessage: string = '';

  constructor(
    private loginService: LoginService,
    private router: Router
  ) {}

  onSubmit(): void {
    if (!this.email || !this.password) {
      this.errorMessage = 'Por favor, completa todos los campos';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    const credentials = {
      email: this.email,
      password: this.password
    };

    this.loginService.login(credentials).subscribe({
      next: (response) => {
        this.isLoading = false;
        this.router.navigate(['/student/classes']);
      },
      error: (error) => {
       
        this.isLoading = false;
        this.errorMessage = ErrorHandlerUtil.handleError(error.error, 'Credenciales incorrectas. Por favor, intenta de nuevo.');
      }
    });
  }
}
