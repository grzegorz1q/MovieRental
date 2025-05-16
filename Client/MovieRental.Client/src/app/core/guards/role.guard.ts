import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth/auth.service';

export const roleGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const allowedRoles = route.data['roles'] as string[];
  const userRole = authService.getRole();
  if(allowedRoles.includes(userRole)){
    return true;
  }
  alert("Nie masz uprawnień do tej strony");
  if(userRole === 'Admin'){
    router.navigateByUrl('/admin');
  }
  else if(userRole === 'Employee'){
    router.navigateByUrl('/employee');
  }
  else{
  router.navigateByUrl('/movies');
  }
  return false;
};
