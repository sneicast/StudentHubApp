import { Routes } from '@angular/router';
import { StudentLayout } from './layouts/student-layout/student-layout';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    {
        path: 'login',
        loadComponent: () => import('./features/login/page/login-page').then(m => m.LoginPage)
    },
    {
        path: 'register',
        loadComponent: () => import('./features/register/page/register-page').then(m => m.RegisterPage)
    },
    {
        path: 'student',
        canActivate: [authGuard],
        component: StudentLayout,
        children:[
            {
                path: 'start',
                loadComponent: () => import('./features/student-start/page/student-start-page').then(m => m.StudentStartPage)
            },
            {
                path: 'classes',
                loadComponent: () => import('./features/student-classes/page/student-classes-page').then(m => m.StudentClassesPage)
            }
        ]
    },
   { path: '**', redirectTo: 'student/start' }
];
