import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { ApiService } from '../../../services/api.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
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
  submitted:boolean;
  constructor(private formBuilder: FormBuilder,
    private _api: ApiService,
    private _auth: AuthService,
    private _router: Router) { }

  ngOnInit() {
       this.registerForm = this.formBuilder.group({
           nombres:['', Validators.required,Validators.minLength(5),Validators.maxLength(50)],
           aPaterno: ['', Validators.required],
           aMaterno: ['', Validators.required],
           fechaNac: ['', Validators.required],
           direccion: ['', Validators.required,Validators.minLength(5),Validators.maxLength(50)],
           telefono: ['', Validators.required,Validators.maxLength(9),Validators.minLength(9)],
           user:  ['', Validators.required,Validators.minLength(4),Validators.maxLength(50)],
           dni: ['', Validators.required,Validators.minLength(8)],
           email: ['', [Validators.required, Validators.email,Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]],
           password: ['', [Validators.required, Validators.minLength(6)]],
           confirmPassword: ['', [Validators.required, Validators.minLength(6)]]
       });
  }

  get f() { return this.registerForm.controls; }

  onSubmit(): void {
    this.errorMessage = '';
    this.submitted = true;
    // stop here if form is invalid
    if (this.registerForm.invalid) {
      this.errorMessage = 'Valida nuevamente que los datos ingresados sean correctos :)';
      return;
    }else{
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
    // this.errorMessage = '';
    // if (this.user && this.password && this.email && this.confirmPassword) {
    //   if (this.password !== this.confirmPassword) {
    //     this.errorMessage = 'Las contraseñas deben coincidir';
    //   } else {
    //     this.loading = true;
    //     this._auth
    //       .register({
    //         dni: this.dni,
    //         nombres:this.nombres,
    //         aPaterno:this.aPaterno,
    //         aMaterno:this.aMaterno,
    //         fechaNac:this.fechaNac,
    //         direccion:this.direccion,
    //         telefono:this.telefono,
    //         email:this.email,
    //         user:this.user,
    //         password:this.password
    //       })
    //       .subscribe(
    //         (res) => {
    //           console.log(res);
    //           this.loading = false;
    //           this._router.navigate(['/login']);
    //         },
    //         (err) => {
    //           this.errorMessage = err.error.message;
    //           this.loading = false;
    //         }
    //       );
    //   }
    // } else {
    //   this.errorMessage = 'Asegurate de llenar todo ;)';
    // }
  }

  canSubmit(): boolean {
    let mailValido = false;
    var EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    // if (this.email.match(EMAIL_REGEX)){
    //   mailValido = true;
    // }

    return  this.user && (this.password==this.confirmPassword) && this.confirmPassword
    && this.dni && this.nombres && this.aPaterno && this.aMaterno && this.fechaNac
    && this.direccion && this.telefono
      ? true
      : false;
  }
}
