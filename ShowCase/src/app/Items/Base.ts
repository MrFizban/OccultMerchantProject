export enum CoinType
{
  CopperCoin,
  SilverCoin,
  GoldCoin,
  PlatinumCoin
}

export class Price
{

   constructor(public value:number = 0, public coin: CoinType = CoinType.CopperCoin) {}

    public toString(){
     return this.value.toString() + " " + CoinType[this.coin];
    }

    private coinToString(){

    }
}

export class Base {

  constructor(
    public id: number = 0,
    public name: string ="",
    public description: string ="",
    public source: string ="",
    public price: Price = new Price(),
) {
  }
}
