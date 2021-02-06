import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/Service/auth.service';
import { SignUp} from '../registration/signUpModel';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  isLoginMode = false;
  error: string= null;
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }
  onSwitchMode(){
    this.isLoginMode = !this.isLoginMode;
  }
  onSubmit(form : NgForm){
    if(!form.valid){
      return;
    }
    console.log(form);

    if(!this.isLoginMode){
      const userSignUpInfo = new SignUp(form.value.fullName,form.value.userName,
        form.value.email,form.value.password,form.value.confirmpassword,[], false);
      console.log(userSignUpInfo);
      this.authService.signUp(userSignUpInfo).subscribe(
        response => {
          this.authService.login(form.value.email, form.value.password);
          console.log(response);
        }, (error :HttpErrorResponse)=> {
          this.error = error.message;
          console.log(error.message);
        }
      );
    }
    else{
      this.authService.login(form.value.email, form.value.password).subscribe(
        response => {
          console.log(response);
        }, (error :HttpErrorResponse)=> {
          this.error = error.message;
          console.log(error.message);
        }
      );
    }
    
  }

}
