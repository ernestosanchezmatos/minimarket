import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { Router } from '@angular/router';
import {FormControl} from '@angular/forms';
import {SelectModel } from '../shared/models/select.model';

@Component({
  selector: 'app-register-products',
  templateUrl: './register-products.component.html',
  styleUrls: ['./register-products.component.scss']
})
export class RegisterProductsComponent implements OnInit {
  nproduct:string='';
  stock:Number;
  price:Number;
  description:String;
  image:String;
  category:'';
  marca:'';
  errorMessage:String='';
  loading:Boolean;
  disableSelect = new FormControl(false);
  seleccionMarca:{id:string,value:string};
  seleccionCategoria:{id:string,value:string};
  ArrMarcasSelect:SelectModel[]=[];
  ArrCategoriasSelect:SelectModel[]=[];
  constructor(
    private _product: ProductService,
    private _router: Router
    ) { }

  ngOnInit(): void {
    this._product.getListMarcasSelect().subscribe(
      (res: any) => {
        console.log(res);
        this.ArrMarcasSelect = res;
        //this.loading = false;
      },
      (err) => {
        console.log(err);
        //this.loading = false;
      }
    );

    this._product.getListCategoriasSelect().subscribe(
      (res: any) => {
        console.log(res);
        this.ArrCategoriasSelect = res;
        //this.loading = false;
      },
      (err) => {
        console.log(err);
        //this.loading = false;
      }
    );
  }
  onSubmit(): void {
    this.errorMessage = '';
    this.loading = true;
    console.log('seleccionCategoria '+JSON.stringify(this.seleccionCategoria))
    console.log('seleccionCategoria '+JSON.stringify(this.seleccionMarca))
        this._product
          .postProducts({
            name: this.nproduct,
            description: this.description,
            image: this.image,
            price: this.price,
            quantity: this.stock,
            categoryId:this.seleccionCategoria.id,
            marcaId:this.seleccionMarca.id
          })
          .subscribe(
            (res) => {
              console.log(res);
              this.loading = false;
              this._router.navigate(['/']);
            },
            (err) => {
              this.errorMessage = err.error.message;
              this.loading = false;
            }
          );

  }
}
