
export enum CoinType {
  PlatinumCoin="PlatinumCoin",
  GoldCoin="GoldCoin",
  SilverCoin="SilverCoin",
  CopperCoin="CopperCoin"
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
}
