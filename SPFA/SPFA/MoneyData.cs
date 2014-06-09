using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPFA
{
    [Serializable]
    public class MoneyData
    {
        double amount;
        String primaryType;
        String date; 

        /*
         * Constructor for MoneyData class which stores an amount, type (categorical variable), and date associated with an expense/income
         * */
        public MoneyData(double amount, String primaryType)
        {
            this.amount = amount;
            this.primaryType = primaryType;
            DateTime now = DateTime.Now;
            date = now.ToString(@"M/d"); // dates formatted differently on different computers 4-28 on this computer 4/28 on desktop
        }

        /*
         * Alternate constructor for Moneydata
         * 
         * Not currently used in this version of the project
         * */
        public MoneyData(double amount, String primaryType, String date)
        {
            this.amount = amount;
            this.primaryType = primaryType;
            this.date = date;
        }

        //Accessors and mutators
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        //Accessors and mutators
        public String PrimaryType
        {
            get { return primaryType; }
            set { primaryType = value; }
        }

        //Accessors and mutators
        public String Date
        {
            get { return date; }
            set { date = value; }
        }

    }


}
