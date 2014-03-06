//Credit.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMV1
{
    class Credit : Deposit
    {
        override public double MonthPay()
        {
            return (amount * 1.0 / months + amount * (percent / 100) / months);
        }
    }
}
