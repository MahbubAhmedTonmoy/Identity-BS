import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {tap} from 'rxjs/operators';

import { SignUp} from '../auth/registration/signUpModel';
import {UserLogInInfo} from '../Model/UserLogInInfo.model';
@Injectable({
    providedIn: 'root'
})

export class AuthService{

    constructor(private http: HttpClient){}

    signUp(signUp: SignUp) {
        return this.http.post(
            'https://localhost:44380/api/Auth/registration',{
                    fullName: signUp.FullName,
                    userName: signUp.UserName,
                    email: signUp.Email,
                    password : signUp.Password,
                    confirmPassword: signUp.ConfirmPassword,
                    role: ["User"],
                    twoFactorEnabled: false
                  
            }
        )
    }

    login(email: string, password: string){
        return this.http.post<LoginResponse>(
            'https://localhost:44380/api/Auth/login',{
                email: email,
                password: password
            }
        ).pipe(
            tap(
                response =>{
                    this.handleAuthentication(response.accessToken, response.refreshToken, response.expires_in);
                }
            )
        );
    }
    private handleAuthentication(
        accessToken: string,
        refreshToken: string,
        expiresIn: string
      ) {
        const expirationDate = new Date(new Date(Date.parse(expiresIn)));
        const userLosinInfo = new UserLogInInfo(accessToken, refreshToken, expirationDate);
        console.log('login info:' + userLosinInfo);
        localStorage.setItem('userData', JSON.stringify(userLosinInfo));
      }
}
export interface LoginResponse{
    //kind: string;
    accessToken: string;
    refreshToken: string;
    expires_in: string;
  }
