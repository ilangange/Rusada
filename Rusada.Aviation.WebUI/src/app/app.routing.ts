import { RouterModule, Routes } from "@angular/router";
import { LoginComponent } from "./components/login/login.component";
import { SightingMainComponent } from "./components/sightings/sighting-main/sighting-main.component";
import { AuthGuardService as AuthGuard } from './auth/auth-guard.service';

const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'sightings', component: SightingMainComponent, canActivate: [AuthGuard] },
    { path: '**', redirectTo: '' }
];

export const AppRoutingModule = RouterModule.forRoot(routes);