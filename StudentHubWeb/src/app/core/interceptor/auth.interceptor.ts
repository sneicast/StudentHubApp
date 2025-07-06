import { HttpInterceptorFn, HttpRequest, HttpHandlerFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { SessionStorageService } from '../service/session-storage.service';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';


export const authInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {
  const sessionStorageService = inject(SessionStorageService);
  const router = inject(Router);
  
  // URLs que no necesitan autenticación
  const publicUrls = ['/api/Student/login', '/api/Student/register', '/api/CreditProgram'];
  
  // Verificar si la URL actual es pública
  const isPublicUrl = publicUrls.some(url => req.url.includes(url));
  
  let authReq = req;
  
  // Solo agregar token si no es una URL pública
  if (!isPublicUrl) {
    const token = sessionStorageService.getToken();
    
    if (token) {
      authReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`)
      });
    }
  }

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      // Si hay error 401 (Unauthorized), limpiar sesión y redirigir
      if (error.status === 401) {
        sessionStorageService.clearSession();
        router.navigate(['/login']);
      }
      
      return throwError(() => error);
    })
  );
};