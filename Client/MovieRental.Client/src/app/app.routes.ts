import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/pages/login/login.component';
import { MoviesListComponent } from './features/movies/pages/movies-list/movies-list.component';
import { RegisterComponent } from './features/auth/pages/register/register.component';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';
import { MovieDetailsComponent } from './features/movies/pages/movie-details/movie-details.component';
import { AddReviewModalComponent } from './features/reviews/components/add-review-modal/add-review-modal.component';
import { LoggedUserInfoComponent } from './shared/logged-user-info/logged-user-info.component';
import { EmployeesListComponent } from './features/employee/pages/employees-list/employees-list.component';
import { RentsDashboardComponent } from './features/rents/pages/rents-dashboard/rents-dashboard.component';
import { ForgotPasswordComponent } from './shared/forgot-password/forgot-password.component';
import { ClientsListComponent } from './features/clients/pages/clients-list/clients-list.component';
import { ClientRentsListComponent } from './features/rents/pages/client-rents-list/client-rents-list.component';
import { GeminiDashboardComponent } from './features/gemini/pages/gemini-dashboard/gemini-dashboard.component';

export const routes: Routes = [
    {
        path: '', redirectTo: 'movies', pathMatch: 'full'
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'movies',
        component: MoviesListComponent
    },
    {
        path: 'employees',
        component: EmployeesListComponent,
        canActivate: [authGuard, roleGuard],
        data: {
            roles: ['Admin']
        }
    },
    {
        path: 'movies/:id',
        component: MovieDetailsComponent
    },
    {
        path: 'movies/:id/reviews',
        component: AddReviewModalComponent
    },
    {
        path: 'account/me',
        component: LoggedUserInfoComponent
    },
    {
        path: 'rents',
        component: RentsDashboardComponent,
        canActivate: [authGuard, roleGuard],
        data: {
            roles: ['Admin', 'Employee']
        }
    },
    {
        path: 'forgot-password',
        component: ForgotPasswordComponent
    },
    {
        path: 'clients',
        component: ClientsListComponent,
        canActivate: [authGuard, roleGuard],
        data: {
            roles: ['Admin', 'Employee']
        }
    },
    {
        path: 'my-rents',
        component: ClientRentsListComponent,
        canActivate: [authGuard, roleGuard],
        data: {
            roles: ['Client']
        }
    },
    {
        path: 'movies-ai',
        component: GeminiDashboardComponent
    }
];
