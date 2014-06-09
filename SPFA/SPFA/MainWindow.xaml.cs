using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
//using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using Microsoft.Win32;

namespace SPFA
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        User currentUser;
        private BinaryFormatter formatter;
        //MoneyDataList moneyInfo = new MoneyDataList();
		
        public MainWindow()
		{
               // FileStream readerFileStream = new FileStream("Admin.txt", FileMode.Open, FileAccess.Read);
                //currentUser = (User)this.formatter.Deserialize(readerFileStream);
                //readerFileStream.Close();
            currentUser = null;
			this.InitializeComponent();
            this.formatter = new BinaryFormatter();

			
		}

        /*
         * 
         * Stores the information in the different fields related to this income entry
         *
         */
        private void income_Button_Click(object sender, RoutedEventArgs e)
        {
            String amt = Income_amount.GetLineText(0);
            String type1 = Income_Prim_Type.GetLineText(0);
            try
            {
                double amount = Convert.ToDouble(amt);
                MoneyData x = new MoneyData(amount, type1);
                currentUser.IncomeList.Add(x);
                update();
            }
            catch
            {
                MessageBox.Show("Enter a valid amount");
            }
            Income_amount.Text = "Enter Amount";
            Income_Prim_Type.Text = "Enter Category";
        }

        /*
         *
         * Stores the information in the different fields related to this expenditure
         * 
         */
        private void expenditures_Button_Click(object sender, RoutedEventArgs e)
        {
            String amt = Expenditures_Amount.GetLineText(0);
            String type1 = Expenditures_Prim_Type.GetLineText(0);
            try
            {
                double amount = Convert.ToDouble(amt);
                MoneyData x = new MoneyData(amount, type1);
                currentUser.ExpensesList.Add(x);
                update();
            }
            catch
            {
                MessageBox.Show("Enter a valid amount");
            }
            Expenditures_Amount.Text = "Enter Amount";
            Expenditures_Prim_Type.Text = "Enter Category";
            
        }

        /*
         * Opens a user file. Currently users are stored as .txt files, but can easily be changed in a future version
         * */
        public void File_Open_Click(object sender, RoutedEventArgs e)
        {
            FileDialog openFile = new OpenFileDialog();
            openFile.Filter = "User Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (openFile.ShowDialog() == true)
            {
                FileStream readerFileStream = new FileStream(openFile.FileName, FileMode.Open, FileAccess.Read);
                currentUser = (User)this.formatter.Deserialize(readerFileStream);
                readerFileStream.Close();
            }
            update_WelcomeBox();

            update();
        }

        /*
         * Allows the user to save as a specific file name. This is how new users are created at the moment.
         * 
         * */
        private void File_Save_As_Click(object sender, RoutedEventArgs e)
        {
            FileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "User File|*.txt";
            saveFile.Title = "Save A User File";
            if (saveFile.ShowDialog() == true)
            {
                if (saveFile.FileName != "")
                {
                    try
                    {
                        currentUser.Username = saveFile.SafeFileName.Substring(0, saveFile.SafeFileName.Length - 4);
                    }
                    catch
                    {
                        currentUser = new User(saveFile.SafeFileName.Substring(0, saveFile.SafeFileName.Length - 4));
                    }
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    FileStream stream = new FileStream(saveFile.SafeFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                    this.formatter.Serialize(stream, currentUser);
                    stream.Close();
                }
            }
            update_WelcomeBox();
            
            
            update();
        }

        /*
         * Exits program
         */
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            //this.Close();
        }

        /*
         * Changes the current user to a new user. Saves current information and attempts to load new information in
         * */
        private void Change_User_Click(object sender, RoutedEventArgs e)
        {
            File_Save_Click(sender, e);
            File_Open_Click(sender, e);
            update_WelcomeBox();
            update();
        }


        //Clears text in textbox when clicked
        private void textBox_clear(object sender, RoutedEventArgs e)
        {
            Expenditures_Amount.Text = string.Empty;
        }
        //Clears text in textbox when clicked
        private void income_clear(object sender, RoutedEventArgs e)
        {
            Income_amount.Text = string.Empty;
        }
        //Clears text in textbox when clicked
        private void expend_cat_clear(object sender, RoutedEventArgs e)
        {
            Expenditures_Prim_Type.Text = string.Empty;
        }
        //Clears text in textbox when clicked
        private void inc_cat_clear(object sender, RoutedEventArgs e)
        {
            Income_Prim_Type.Text = string.Empty;
        }
        //Clears text in textbox when clicked
        private void event_name_clear(object sender, RoutedEventArgs e)
        {
            Event_name.Text = string.Empty;
        }
        //Clears text in textbox when clicked
        private void amt_need_clear(object sender, RoutedEventArgs e)
        {
            Amount_needed.Text = string.Empty;
        }

        /*
         * Saves the current information to the current user. Opens the file dialog if there is no current user
         * 
         * Currently saves to a specific directory. Need to change this in the future.
         * */
        private void File_Save_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser == null)
            {
                File_Save_As_Click(sender, e);
            }
            else
            {
                FileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "User File|*.txt";

                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                FileStream stream = new FileStream(currentUser.Username + ".txt", FileMode.Create, FileAccess.Write, FileShare.None);
                this.formatter.Serialize(stream, currentUser);
                stream.Close();
            }
            update_WelcomeBox();
            
        }

        /*
         * Updates the text in the welcome message at the top of the window
         * */
        private void update_WelcomeBox()
        {
            try
            {
                WelcomeBox.Text = "Welcome " + currentUser.Username + "!";
            }
            catch
            {
                //Error handler
            }
        }

        /*
         *Updates the recent transactions and income lists 
         * 
         */
        public void update()
        {
            ObservableCollection<MoneyData> result = new ObservableCollection<MoneyData>();
            MoneyData myColl;
            for(int i= 0; i < currentUser.ExpensesList.Count; i++)
            {
                myColl = new MoneyData(0,"0");
                myColl.Amount = currentUser.ExpensesList[i].Amount;
                myColl.PrimaryType = currentUser.ExpensesList[i].PrimaryType.ToString();
                myColl.Date = currentUser.ExpensesList[i].Date;//.ToString();
                result.Add(myColl);
            }

            ObservableCollection<MoneyData> result2 = new ObservableCollection<MoneyData>();
            for (int i = 0; i < currentUser.IncomeList.Count; i++)
            {
                myColl = new MoneyData(0, "0");
                myColl.Amount = currentUser.IncomeList[i].Amount;
                myColl.PrimaryType = currentUser.IncomeList[i].PrimaryType.ToString();
                myColl.Date = currentUser.IncomeList[i].Date;//.ToString();
                result2.Add(myColl);
            }
            transactionList.ItemsSource = result;
            incomeList.ItemsSource = result2;
        }

        /*
         * Opens the window for editing a specific item in the selected list
         * */
        private void income_editWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                EditWindow ew = new EditWindow(currentUser, this, 1, incomeList.SelectedIndex);
                ew.Show();
            }
            catch
            {
                //Error handling
            }
        }

        /*
 * Opens the window for editing a specific item in the selected list
 * */
        private void transaction_editWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                EditWindow ew = new EditWindow(currentUser, this, 0, transactionList.SelectedIndex);
                ew.Show();
                //EditWindow ew = new EditWindow(((MoneyData)transactionList.SelectedItems[0]), this);
               // ew.Show();
               // MessageBox.Show(((MoneyData)transactionList.SelectedItems[0]).PrimaryType);
            }
            catch
            {
                //Error handling
            }
        }


        /*
         * Opens the event calendar if someone tries to click on the calendar itself
         * Doesn't work when specific days are clicked in the calendar
         * 
         * */
        private void Calendar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Calendar cal = new Calendar(currentUser);
                cal.Show();
                //open calendar
            }
            catch
            {
                //Error handling
            }
        }

        /*
         * This is triggered when someone clicks the view event calendar button.
         * It opens the event calendar
         * 
         * Poorly named, should rename in a later version
         * */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Calendar cal = new Calendar(currentUser);
                cal.Show();
                //open calendar
            }
            catch
            {
                //Error handling
            }
        }


        /*
         * This opens up the graph window.
         * 
         * Poorly named, should rename in a later version
         * */
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Graph g = new Graph(currentUser.ExpensesList, currentUser.IncomeList);
                g.Show();
            }
            catch
            {
                //Error handling
            }
        }


        /*
         * This method should open a help wizard. Currently just gives a popup
         * */
        private void help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is where the help functionality will go");
        }

        /*
         * This method generates a budget for a user based on what they enter into the above fields
         * It gives the amount of time required in terms of "Pay periods" at the current version
         * 
         * Calculations are based on average spending and income values.
         * 
         * */
        private void Generate_Budget_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser.IncomeList.Count > 0 && currentUser.ExpensesList.Count > 0)
            {
                try
                {
                    String eventName = Event_name.Text;
                    double eventAmount = Convert.ToDouble(Amount_needed.Text);
                    double numPurchases = 0;
                    double totalSpent = 0;
                    for (int i = 0; i < currentUser.ExpensesList.Count; i++)
                    {
                        numPurchases++;
                        totalSpent += currentUser.ExpensesList[i].Amount;
                    }
                    double averageSpending = (totalSpent / numPurchases);

                    double numPaychecks = 0;
                    double totalIncome = 0;
                    for (int i = 0; i < currentUser.IncomeList.Count; i++)
                    {
                        numPaychecks++;
                        totalIncome += currentUser.IncomeList[i].Amount;
                    }
                    double averageIncome = (totalIncome / numPaychecks);

                    double averageSavings = averageIncome - averageSpending;

                    double timeNeeded = Math.Ceiling(eventAmount / averageSavings);

                    MessageBox.Show(String.Format("{0}", timeNeeded) + " pay period(s) needed to budget for " + eventName);
                }
                catch
                {
                    MessageBox.Show("Enter valid values in required fields");
                }
            }
        }
	}
}