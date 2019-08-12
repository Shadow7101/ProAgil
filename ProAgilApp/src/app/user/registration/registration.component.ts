import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  titulo = 'Cadastro';
  registerForm: FormGroup;
  user: User;

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private authService: AuthService,
    private router: Router) {
  }

  ngOnInit() {
    this.validation();
  }

  validation(): void {
    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', Validators.required],
      passwords: this.fb.group({
        password: ['', [Validators.required, Validators.minLength(4)]],
        confirmPassword: ['', Validators.required]
      }, { validator: this.compararSenha })
    });
  }
  compararSenha(fg: FormGroup): void {
    const confirmarSenhaCtrl = fg.get('confirmPassword');
    if (confirmarSenhaCtrl.errors == null || 'mismatch' in confirmarSenhaCtrl.errors) {
      if (confirmarSenhaCtrl.value !== fg.get('password').value) {
        confirmarSenhaCtrl.setErrors({ mismatch: true });
      } else {
        confirmarSenhaCtrl.setErrors(null);
      }
    }
  }
  cadastrarUsuario(): void {
    if (!this.registerForm.valid) { return; }

    this.user = Object.assign({ password: this.registerForm.get('passwords.password').value },
      this.registerForm.value);

    this.authService.Register(this.user).subscribe(response => {
      console.log(response);
      this.toastr.success('Seu registro foi realizado com sucesso!', 'Sucesso!');
      this.router.navigate(['/user/login']);
    }, error => {
      console.error(error);
      const erro = error.error;
      erro.forEach(element => {
        switch (element.code) {
          case 'DuplicateUserName':
            this.toastr.error(error.message, 'Cadastro já realizado!');
            break;
          default:
            this.toastr.error(error.message, `Erro ao registrar! CODE: ${element.code}`);
        }
      });
    });

  }

}
