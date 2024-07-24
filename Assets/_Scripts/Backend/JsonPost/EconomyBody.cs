namespace Farmanji.Data
{
    [System.Serializable]
    public class EconomyBody : PostBody
    {
        public int gems;
        public int coins;

        public static EconomyBody CreateCurrenciesBody(int _coins, int _gems)
        {
            var currenciesBody = new EconomyBody()
            {
                coins = _coins,
                gems = _gems
            };
            return currenciesBody;
        }
    }
}