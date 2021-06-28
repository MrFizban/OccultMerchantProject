
export enum CoinType {
  PlatinumCoin=3,
  GoldCoin=2,
  SilverCoin=1,
  CopperCoin=0
}


export class Price {
  public value: number;
  public coin: CoinType;

  constructor(_value: number = 0, _coin: CoinType = CoinType.SilverCoin) {
    this.value = _value;
    this.coin = _coin;
  }

  toString(): string{
    return this.value +" " + this.coin;
  }

  public clone(): any {
    var cloneObj = new Price() ;
    for (var attribut in this) {
      if (typeof this[attribut] === "object") {
        // @ts-ignore
        cloneObj[attribut] = this[attribut].clone();
      } else {
        // @ts-ignore
        cloneObj[attribut] = this[attribut];
      }
    }
    return cloneObj;
  }
}
