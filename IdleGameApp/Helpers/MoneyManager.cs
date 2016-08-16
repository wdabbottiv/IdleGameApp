using System.Numerics;

namespace IdleGameApp.Helpers
{
    public class MoneyManager
    {
        public BigInteger TotalMoney = new BigInteger(0);
        public int ClickValue = 1;

        internal string GetPrettyTotal()
        {
            return TotalMoney.ToString("#,0");
        }
    }
}