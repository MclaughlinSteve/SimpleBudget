using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonthCalendar;

namespace SPFA
{
    [Serializable]
    public class User
    {
        private String username;
        List<MoneyData> expensesList;
        List<MoneyData> incomeList;
        List<Appointment> userEvents;
        /*
         * Constructor for User class
         * 
         * This class stores a username with a slew of important information associated with that user
         * 
         * It stores the user's expenses, income, and any events that user has on their calendar.
         * */
        public User(String name)
        {
            username = name;
            expensesList = new List<MoneyData>();
            incomeList = new List<MoneyData>();
            userEvents = new List<Appointment>();
        }

        //Accessors and mutators
        public String Username
        {
            get { return username; }
            set { username = value; }
        }

        //Accessors and mutators
        public List<MoneyData> ExpensesList
        {
            get { return expensesList; }
            set { expensesList = value; }
        }

        //Accessors and mutators
        public List<MoneyData> IncomeList
        {
            get { return incomeList; }
            set { incomeList = value; }
        }

        //Accessors and mutators
        public List<Appointment> UserEvents
        {
            get { return userEvents; }
            set { userEvents = value; }
        }

    }
    
}
