import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Products, Product } from '../shared/models/product.model';
import {SelectModel } from '../shared/models/select.model';
import { environment } from '../../environments/environment';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private url = environment.apiUrl;

  constructor(private http: HttpClient, private _api: ApiService) {}

  getAllProducts(limitOfResults = 9, page): Observable<Products> {
    return this.http.get<Products>(this.url + 'Productos/listProducts', {
      params: {
        limit: limitOfResults.toString(),
        page: page,
      },
    });
  }

  getListMarcasSelect(): Observable<SelectModel> {
    return this.http.get<SelectModel>(this.url + 'Productos/getMarcasSelect');
  }

  getListCategoriasSelect(): Observable<SelectModel> {
    return this.http.get<SelectModel>(this.url + 'Productos/getCategoriasSelect');
  }

  getSingleProduct(id: Number): Observable<any> {
    console.log(id);
    return this._api.getTypeRequest('Productos/' + id);
  }

  postProducts(product:any): Observable<any> {
    return this._api.postTypeRequest('Productos/registrarProducto', {
      name: product.name,
      categoryId: product.categoryId,
      description: product.description,
      image: product.image,
      price: product.price,
      quantity: product.quantity,
      marcaId:product.marcaId
    });
  }
}
