import { LoginResponse } from './../../models/seguridad/login-response';
import { LoginRequest } from 'src/app/models/seguridad/login-request';
import { LoginService } from './../../services/seguridad/login.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { Router, RouterLink } from '@angular/router';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';
import { GeneralConstant } from '@shared/general-constant';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    RouterLink
    ]
})
export class LoginPage implements OnInit {
  loginRequest: LoginRequest = new LoginRequest();
  constructor(
    private loginService: LoginService,
    private router: Router
    ) { }

  ngOnInit() {

  }

  public async autenticar() {
    await lastValueFrom(this.loginService.authenticate(this.loginRequest))
          .then(loginResponse => {
            localStorage.setItem(GeneralConstant.TOKEN_KEY, loginResponse.token);
            this.router.navigate(['/paciente']);
          })
          .catch(error => {
            localStorage.removeItem(GeneralConstant.TOKEN_KEY);
          });
  }

}
