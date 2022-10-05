import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  dni: string;
  nombres:string;
  aPaterno:string;
  aMaterno:string;
  fechaNac:Date;
  direccion:string;
  telefono:string;
  email:string='';
  user:string;
  password:string='';

  error = '';
  loading = false;

  constructor(private _auth: AuthService, private _router: Router) {}

  ngOnInit(): void {}

  onSubmit(): void {
    this.loading = true;
    this.error = '';    
    if (!this.email || !this.password) {
      this.error = 'Asegurate de llenar todo el campo;)';
    } else {  
      this._auth
        .login({          
          email:this.email,         
          password:this.password
          })
        .subscribe(
          (res) => {
            console.log(res);
            this.loading = false;
            this._router.navigate(['/']);
          },
          (err) => {
            console.log(err);            
            this.error = err.error.message;
            this.loading = false;
          }
        );
    }
  }

  canSubmit(): boolean {
    //return true;
    return this.email.length > 0 && this.password.length > 0;
  }
}
