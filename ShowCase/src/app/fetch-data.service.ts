import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Potion, SpellName} from "./Items/Potion";
import {Spell} from "./Items/Spell";
import {Price} from "./Items/Base";
import {Shop} from "./Items/Shop";


@Injectable({
  providedIn: 'root'
})
export class FetchDataService {
  baseurl: string = "https://localhost:5001/";


  constructor(private http: HttpClient) {
  }

  private getAllRequest(url: string) {
    let headers: HttpHeaders = new HttpHeaders();
    headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.get<Array<any>>(this.baseurl + url, {headers: headers});
  }

  getShop(id: number) {
    let headers: HttpHeaders = new HttpHeaders();
    headers.set('Content-Type', 'application/json; charset=utf-8');
    let shop = new Shop();
    shop.id = id;
    shop.filter.names.push("id");
    let params = new HttpParams();
    params.append("id", id)
    params.set("filter.names", 'id')
    return this.http.get<Shop[]>(this.baseurl + "shop/gettAll", {
      headers: headers, params: {
        id: id.toString(),
        "filter.names": "name"
      }
    });
  }

  getAllPotion() {
    return this.getAllRequest("potion/gettAll")
  }

  getAllSpell() {
    return this.getAllRequest("spell/gettAll")
  }

  getAllShop() {
    return this.getAllRequest("shop/gettAll");
  }

  private postRequest(url: string, body: any) {
    let headers: HttpHeaders = new HttpHeaders();
    headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.post<any>(this.baseurl + url, body, {headers: headers});
  }

  postPotion(body: Potion) {
    return this.postRequest("potion", body)
  }

  postSpell(body: Spell) {
    return this.postRequest("spell", body);
  }

  private updateRequest(url: string, body: any) {
    let headers: HttpHeaders = new HttpHeaders();
    headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.put(this.baseurl + url, body);
  }

  updatePotion(potion: Potion) {
    return this.updateRequest("potion", potion);
  }

  updateSpell(spells: Spell) {
    return this.updateRequest("spell", spells);
  }

  updateShopPotion(stock: Shop) {
    return this.postRequest("shop/addPotion", stock);
  }

  private deleteRequest(url: string, id: number) {
    console.log("id:\t" + id);
    let str: string = this.baseurl + url + "/" + id.toString();
    console.log(str);
    return this.http.delete(this.baseurl + url + "/" + id.toString())
  }

  deletePotion(id: number) {
    console.log("id:\t" + id);
    return this.deleteRequest("potion", id);
  }

  deleteSpell(id: number) {
    return this.deleteRequest("spell", id);

  }

  deletePotionFromStock(idShop: number, idPotio: number) {

    let str: string = this.baseurl + "shop/potionStock/" + idShop.toString() + "/" + idPotio.toString();
    return this.http.delete(str);
  }


  updateShop(shop: Shop) {
    return this.updateRequest("shop", shop);
  }

  postShop(tmp: Shop) {
    return this.postRequest("shop", tmp);
  }

  deleteShop(shop: Shop) {
    console.log(shop);
    return this.deleteRequest("shop", shop.id);
  }
}
