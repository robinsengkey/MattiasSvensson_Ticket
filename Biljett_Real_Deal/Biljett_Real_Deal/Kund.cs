using System;
using System.Collections.Generic;
using System.Text;

namespace Biljett_Real_Deal
{
    class Kund
    {
        public string namn;
        public int kostnad;         //Alla attribut och variabler gällande "Kund"
        public int ålder;
        public string konsert;
        public string order;

        public Kund(string aNamn, int aKostnad, int aÅlder, string aKonsert)
        {

            {
                namn = aNamn;
                kostnad = aKostnad;
                ålder = aÅlder;
                konsert = aKonsert;
            }
        }


        public override string ToString()
        {
            return namn + "$" + ålder + "$" + kostnad + "$" + konsert + "$" + order;
        }

    }
}
