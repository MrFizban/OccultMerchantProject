import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Weapons} from "./Items/Weapons"
import { Observable, throwError } from 'rxjs';
import {Item, Shop} from "./Items/Shop";
import {ActivatedRoute, Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class FetchDataService {

  public showFiller = false;

  public weapons : Weapons[] = new Array<Weapons>();
  private _baseUrl : string;
  shopList: Array<Shop> = new Array<Shop>()
  constructor(private http: HttpClient,@Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute )
  {
    this._baseUrl = baseUrl;

  }

  removeA(arr) {
    var what, a = arguments, L = a.length, ax;
    while (L > 1 && arr.length) {
      what = a[--L];
      while ((ax= arr.indexOf(what)) !== -1) {
        arr.splice(ax, 1);
      }
    }
    return arr;
  }

  giveMeWeapons(): Observable<Weapons[]>{
    return this.http.get<Weapons[]>(this._baseUrl + 'giveMeWeapons');
  }

  addWeapons(wpn:Weapons){
    const headers: HttpHeaders = new  HttpHeaders();
    headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.post<Weapons>(this._baseUrl + 'giveMeWeapons', wpn, {headers:headers} );

  }

  updateWeapond(wpn:Weapons){
    const headers: HttpHeaders = new  HttpHeaders();
    headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.put<Weapons>(this._baseUrl + 'giveMeWeapons', wpn, {headers:headers} ).subscribe();
  }

  deleteWeapons(wpn:Weapons){
    const headers: HttpHeaders = new  HttpHeaders();
    headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.delete(this._baseUrl + 'giveMeWeapons/' + wpn.id.toString(), );

  }

  getAllShop(){
    return this.http.get<Shop[]>(this._baseUrl + 'giveMeShops');
  }

  insertShop(shop:Shop){
    const headers: HttpHeaders = new  HttpHeaders();
    headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.post<Shop>(this._baseUrl + 'giveMeShops', shop, {headers:headers} );
  }

  updateShop(shop:Shop){
    const headers: HttpHeaders = new  HttpHeaders();
    headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.put<Shop>(this._baseUrl + 'giveMeShops', shop, {headers:headers} ).subscribe();
  }

  addItemToShop(shop:Shop){
    const headers: HttpHeaders = new  HttpHeaders();
    headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.put<Shop>(this._baseUrl + 'giveMeShops/addItems/', shop, {headers:headers} ).subscribe();
  }

  deleteShop(shop:Shop){
    const headers: HttpHeaders = new  HttpHeaders();
    return this.http.delete(this._baseUrl + 'giveMeShops/' + shop.id.toString());
  }

  deleteItems(item:Item,shop:Shop){
    const headers: HttpHeaders = new  HttpHeaders();
    return this.http.delete(this._baseUrl + 'giveMeShops/item/' +  shop.id.toString() + "/" + item.id.toString());
  }


}


