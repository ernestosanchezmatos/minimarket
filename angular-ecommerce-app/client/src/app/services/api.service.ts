import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient,HttpClientModule,HttpHeaders } from '@angular/common/http';

import { map } from 'rxjs/operators';

const httpOption = {
  headers :new HttpHeaders({
    'Content-Type' : 'application/json',
    'Authorization': 'Bearer'
  })
 }

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl = environment.apiUrl;

  constructor(private _http: HttpClient) {}

  getTypeRequest(url: string) {
    return this._http.get(`${this.baseUrl}${url}`).pipe(
      map((res) => {
        return res;
      })
    );
  }
  postTypeRequest(url: string, payload: any) { 
    return this._http.post(`${this.baseUrl}${url}`, payload).pipe(
      map((res) => {        
        console.log(res);
        return res;
      })
    );
  }
  putTypeRequest(url: string, payload: any) {
    return this._http.put(`${this.baseUrl}${url}`, payload).pipe(
      map((res) => {
        return res;
      })
    );
  }
}
