//Deposit.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMV1
{
    class Deposit
    {
        public int amount;
        public int months;
        public double percent;
        public Human client;
        public Human manag;
      //  public Manager manag;
      virtual public double MonthPay(){
            return amount * percent*1.0 / 100 / 12; ;
        }
       
    }
}
