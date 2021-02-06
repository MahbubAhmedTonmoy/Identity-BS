import {NgModule} from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../auth/login/login.component';
import { RegistrationComponent } from '../auth/registration/registration.component';

const appRoute: Routes = [
    { path: '', redirectTo: '', pathMatch: 'full'},
    { path: 'registration', component: RegistrationComponent},
    { path: 'login', component: LoginComponent}
]

@NgModule({
    imports: [RouterModule.forRoot(appRoute)],
    exports : [RouterModule]
})
export class AppRoutingModule{

}