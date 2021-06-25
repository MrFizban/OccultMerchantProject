import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Weapons} from "./Items/Weapons"
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FetchDataService {
  public weapons : Weapons[] = new Array<Weapons>();
  private _baseUrl : string;
  constructor(private http: HttpClient,@Inject('BASE_URL') baseUrl: string)
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

  updateWeapons(wpn:Weapons){
    console.log("post angular");
    console.log(wpn)
    return this.http.post<Weapons>(this._baseUrl + 'putWeapons', wpn);
  }



}


