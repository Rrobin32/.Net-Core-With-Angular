import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { LoginService } from '../../services/login/login.service';
import { TokenStorageService } from '../../services/tokenstorage/tokenstorage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup | any;
  isLoggedIn = false;
  isLoginFailed = false;
  roles: string[] = [];
  title = 'login';

  constructor(private router: Router, private service: LoginService, private tokenStorage: TokenStorageService) {
    this.loginForm = new FormGroup({
      userName: new FormControl(),
      password: new FormControl()
    });
  }

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
      //this.roles = this.tokenStorage.getUser().roles;
    }
  }

  onSubmit() {
    if (!this.loginForm.valid) {
      return;
    }
    else {
      let queryString = "?userName=" + this.loginForm.value.userName + "&password=" + this.loginForm.value.password;

      console.log(queryString);
      this.service.login(queryString).subscribe(res => {
        if (res.ResponseMessage[0].CustomCode > 0) {
          this.errorResposeMessage(res); 
        }
        else {
          this.tokenStorage.saveToken(res.ResponseData.LoginResponse.Token.toString());
          this.isLoginFailed = false;
          this.isLoggedIn = true;
          this.router.navigate(['/user'])
        }
      },
        (error: any) => {
          if (error.status == 401 || error.status == 0) {
            alert('Something went wrong!!');
          }  
          this.isLoginFailed = true;
        }
      );
    }    
  }

  errorResposeMessage(res: any) {
    var errorMsg = "";
    for (let i = 0; i < res.ResponseMessage.length; i++) {
      errorMsg += res.ResponseMessage[i].ErrorMessage + "\n"
    }
    alert(errorMsg.trimEnd());
  }
}
