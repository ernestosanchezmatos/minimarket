import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { ApiService } from '../../../services/api.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  dni: string;
  nombres:string;
  aPaterno:string;
  aMaterno:string;
  fechaNac:Date;
  direccion:string;
  telefono:string;
  email:string;
  user:string;
  password:string='';
  confirmPassword:string = '';
  errorMessage = '';
  loading = false;
  chckTermCond=true;
  constructor(
    private _api: ApiService,
    private _auth: AuthService,
    private _router: Router
  ) {}

  ngOnInit(): void {}

  onSubmit(): void {
    this.errorMessage = '';
    if (this.user && this.password && this.email && this.confirmPassword) {
      if (this.password !== this.confirmPassword) {
        this.errorMessage = 'Las contraseñas deben coincidir';
      } else {
        this.loading = true;
        this._auth
          .register({
            dni: this.dni,
            nombres:this.nombres,
            aPaterno:this.aPaterno,
            aMaterno:this.aMaterno,
            fechaNac:this.fechaNac,
            direccion:this.direccion,
            telefono:this.telefono,
            email:this.email,
            user:this.user,
            password:this.password
          })
          .subscribe(
            (res) => {
              console.log(res);
              this.loading = false;
              this._router.navigate(['/login']);
            },
            (err) => {
              this.errorMessage = err.error.message;
              this.loading = false;
            }
          );
      }
    } else {
      this.errorMessage = 'Asegurate de llenar todo ;)';
    }
  }

  canSubmit(): boolean {
    console.log(this.chckTermCond);
    return this.chckTermCond && this.user && this.email && this.password && this.confirmPassword
      ? true
      : false;
  }
}