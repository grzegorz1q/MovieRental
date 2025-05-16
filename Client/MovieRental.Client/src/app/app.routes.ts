import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/pages/login/login.component';
import { MoviesListComponent } from './features/movies/pages/movies-list/movies-list.component';

export const routes: Routes = [
    {
        path: '', redirectTo: 'movies', pathMatch: 'full'
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'movies',
        component: MoviesListComponent
    }
];
