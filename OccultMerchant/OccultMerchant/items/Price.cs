namespace OccultMerchant.items
{
    public enum CoinType
    {
        PlatinumCoin,
        GoldCoin,
        SilverCoin,
        CopperCoin
    }
    
    public struct Price
    {
        public int value { get; set; }
        public CoinType coin { get; set; }

       public Price(int _value, CoinType _coin)
        {
            this.value = _value;
            this.coin = _coin;
        }
    }

    
}