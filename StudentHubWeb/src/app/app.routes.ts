import { Routes } from '@angular/router';
import { StudentLayout } from './layouts/student-layout/student-layout';

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
        component: StudentLayout,
        children:[
            {
                path: 'start',
                loadComponent: () => import('./features/student-start/page/student-start-page').then(m => m.StudentStartPage)
            }
        ]
    },
   { path: '**', redirectTo: 'student/start' }
];
