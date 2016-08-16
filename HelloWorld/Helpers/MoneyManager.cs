using System;
using System.Numerics;

namespace HelloWorld
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