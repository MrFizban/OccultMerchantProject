import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Weapons} from "./Items/Weapons"

@Injectable({
  providedIn: 'root'
})
export class FetchDataService {
  public weapons : Weapons[];
  private _baseUrl : string;
  constructor(private http: HttpClient,@Inject('BASE_URL') baseUrl: string)
  {
    this._baseUrl = baseUrl;
    this.giveMeWeapons();
  }

  giveMeWeapons(){
    this.http.get<Weapons[]>(this._baseUrl + 'giveMeWeapons').subscribe(result => {
      this.weapons = result;
    }, error => console.error(error));
  }

}


