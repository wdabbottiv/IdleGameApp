using System.Numerics;

namespace IdleGameApp.Helpers
{
    public class MoneyManager
    {
        public BigInteger TotalMoney = new BigInteger(0);
        public int ClickValue = 1;

        public string GetPrettyTotal()
        {
            return $"${TotalMoney}";
        }
    }
}