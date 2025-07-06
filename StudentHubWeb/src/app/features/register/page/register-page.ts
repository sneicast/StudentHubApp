import { Component, OnInit } from '@angular/core';
import { RegisterRequestDto } from '../dtos/register-request.dto';
import { CreditProgramDto } from '../../../core/dtos/credit-program.dto';
import { CreditProgramService } from '../../../core/service/credit-program-service';
import { RegisterService } from '../service/register-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-page',
  imports: [FormsModule, CommonModule],
  templateUrl: './register-page.html',
  styleUrl: './register-page.css'
})
export class RegisterPage implements OnInit {
  registerData: RegisterRequestDto = {
    name: '',
    surnames: '',
    email: '',
    password: '',
    creditProgramId: 0
  }

  creditPrograms: CreditProgramDto[] = [];
  isLoading: boolean = false;

  errorMessage: string = '';
  confirmPassword: string = '';
  
   showSuccessModal: boolean = false;
  

  constructor(private creditProgramService: CreditProgramService, private registerService: RegisterService, private router: Router) {}

  ngOnInit(): void {
    this.loadCreditPrograms();
  }

  private loadCreditPrograms(): void {
    this.isLoading = true;
    this.creditProgramService.getCreditPrograms().subscribe({
      next: (programs) => {
        this.creditPrograms = programs;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading credit programs:', error);
        this.errorMessage = 'Error loading credit programs';
        this.isLoading = false;
      }
    });
  }

  onSubmit(): void {
    console.log(this.registerData);
    if (!this.validateForm()) {
      return;
    }
    this.isLoading = true;
    this.errorMessage = '';

    // Aquí harías la llamada al servicio de registro
    console.log('Datos de registro:', this.registerData);
    this.registerService.register(this.registerData).subscribe({
      next: (response) => {
        this.isLoading = false;
         this.showSuccessModal = true;
      },
      error: (error) => {
        this.errorMessage = 'Error al registrar. Por favor, intenta de nuevo.';
        this.isLoading = false;
      }
    });
  }
  closeModalAndRedirect(): void {
    this.showSuccessModal = false;
    this.router.navigate(['/login']);
  }
  onCreditProgramChange(event: any): void {
    this.registerData.creditProgramId = parseInt(event.target.value);
  }
  private validateForm(): boolean {
    // Validar campos requeridos
    if (!this.registerData.name.trim()) {
      this.errorMessage = 'El nombre es requerido';
      return false;
    }

    if (!this.registerData.surnames.trim()) {
      this.errorMessage = 'Los apellidos son requeridos';
      return false;
    }

    if (!this.registerData.email.trim()) {
      this.errorMessage = 'El correo electrónico es requerido';
      return false;
    }

    if (!this.isValidEmail(this.registerData.email)) {
      this.errorMessage = 'El correo electrónico no es válido';
      return false;
    }

    if (!this.registerData.password) {
      this.errorMessage = 'La contraseña es requerida';
      return false;
    }

    if (this.registerData.password.length < 8) {
      this.errorMessage = 'La contraseña debe tener al menos 8 caracteres';
      return false;
    }

    if (this.registerData.password !== this.confirmPassword) {
      this.errorMessage = 'Las contraseñas no coinciden';
      return false;
    }

    if (this.registerData.creditProgramId === 0) {
      this.errorMessage = 'Debes seleccionar un programa académico';
      return false;
    }

    return true;
  }
  private isValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  }
}
