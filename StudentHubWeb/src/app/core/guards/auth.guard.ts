import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { SessionStorageService } from '../service/session-storage.service';

export const authGuard: CanActivateFn = (route, state) => {
  const sessionStorageService = inject(SessionStorageService);
  const router = inject(Router);

  if (sessionStorageService.hasValidSession()) {
    return true;
  }

  // Redirigir al login si no hay sesión válida
  router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
  return false;
};