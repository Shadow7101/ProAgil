import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/_models/User';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';
import { FormBuilder } from '@angular/forms';
import { Login } from 'src/app/_models/Login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  titulo = 'Login';
  model: Login;

  constructor(
    private toastr: ToastrService,
    private authService: AuthService,
    public router: Router) {
  }


  ngOnInit() {
    this.model = new Login();
  }

  login(): void {
    this.authService.Login(this.model)
      .subscribe(response => {
        // console.log(response);
        this.router.navigate(['/dashboard']);
      }, error => {
        console.error(error);
        this.toastr.error('Não foi possivel acessar o sistema!', 'Acesso negado');
      });
  }

}
