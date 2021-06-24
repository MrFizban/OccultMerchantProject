
export enum CoinType {
  PlatinumCoin,
  GoldCoin,
  SilverCoin,
  CopperCoin
}


export class Price {
  public value: number;
  public coin: CoinType;

  constructor(_value: number = 0, _coin: CoinType = 2) {
    this.value = _value;
    this.coin = _coin;
  }
}
