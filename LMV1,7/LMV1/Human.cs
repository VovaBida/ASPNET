using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMV1
{
    class Human
    {
        static int count=0;
        public string name;
        public string surname;
        public List<Deposit> deposit_history;
        public List<Credit> credit_history;

        public Human(string n="",string s="")
        {
            name = n;
            surname = s;
            count++;
            deposit_history = new List<Deposit>(0);
            credit_history = new List<Credit>(0);
        }
        public override bool Equals(object obj)
        {
            Human hum = (Human)obj;
            return ((hum.name == this.name)&&(hum.surname==this.surname));
        }
        public override int GetHashCode()
        {
            return (this.name + " " + this.surname).GetHashCode();
        }
    }

    /*class Manager : Human {
    
    }*/
}
