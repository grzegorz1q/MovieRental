import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/pages/login/login.component';
import { MoviesListComponent } from './features/movies/pages/movies-list/movies-list.component';
import { RegisterComponent } from './features/auth/pages/register/register.component';
import { AdminDashboardComponent } from './features/admin/pages/admin-dashboard/admin-dashboard.component';
import { EmployeeDashboardComponent } from './features/employee/pages/employee-dashboard/employee-dashboard.component';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';
import { MovieDetailsComponent } from './features/movies/pages/movie-details/movie-details.component';
import { AddReviewModalComponent } from './features/reviews/components/add-review-modal/add-review-modal.component';
import { LoggedUserInfoComponent } from './shared/logged-user-info/logged-user-info.component';

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
        path: 'admin',
        component: AdminDashboardComponent,
        canActivate: [authGuard, roleGuard],
        data: {
            roles: ['Admin']
        }
    },
    {
        path: 'employee',
        component: EmployeeDashboardComponent,
        canActivate: [authGuard, roleGuard],
        data: {
            roles: ['Employee']
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
    }
];
