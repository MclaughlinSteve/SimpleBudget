using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SPFA
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        User usr;
        MainWindow mw;
        int listCheck;
        int si;

        /*
         * Constructor for editWindow
         * Populates the textboxes with the current values of the item to be modified
         * */
        public EditWindow(User currentUser, MainWindow m, int listCheck, int selectedIndex)
        {
            si = selectedIndex;
            this.listCheck = listCheck;
            mw = m;
            usr = currentUser;
            this.InitializeComponent();
            if (listCheck > 0)
            {
                amount_value.Text = usr.IncomeList[si].Amount.ToString();
                cat_value.Text = usr.IncomeList[si].PrimaryType;
                date_value.Text = usr.IncomeList[si].Date;
            }
            else
            {
                amount_value.Text = usr.ExpensesList[si].Amount.ToString();
                cat_value.Text = usr.ExpensesList[si].PrimaryType;
                date_value.Text = usr.ExpensesList[si].Date;
            }
        }

        //Clears text in textbox when clicked
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            amt_tb.Text = string.Empty;
        }
        //Clears text in textbox when clicked
        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            cat_tb.Text = string.Empty;
        }
        //Clears text in textbox when clicked
        private void TextBox_GotFocus_2(object sender, RoutedEventArgs e)
        {
            date_tb.Text = string.Empty;
        }

        /*
         * This updates the value associated with the user with the new value in the amount field
         * */
        private void amt_update_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listCheck > 0)
                {
                    usr.IncomeList[si].Amount = Convert.ToDouble(amt_tb.Text);
                }
                else
                {
                    usr.ExpensesList[si].Amount = Convert.ToDouble(amt_tb.Text);
                }
                amount_value.Text = amt_tb.Text;
                amt_tb.Text = "New value";
                //editValue.Amount = Convert.ToDouble(amt_tb.Text);
                mw.update();
            }
            catch
            {
                //Error handling
            }
        }

        /*
        * This updates the value associated with the user with the new value in the category field
        * */
        private void cat_update_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listCheck > 0)
                {
                    usr.IncomeList[si].PrimaryType = cat_tb.Text;
                }
                else
                {
                    usr.ExpensesList[si].PrimaryType = cat_tb.Text;
                }
                cat_value.Text = cat_tb.Text;
                cat_tb.Text = "New value";
                mw.update();
            }
            catch
            {
                //Error handling
            }
        }

        /*
 * This updates the value associated with the user with the new value in the date field
 * */
        private void date_update_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listCheck > 0)
                {
                    usr.IncomeList[si].Date = date_tb.Text;
                }
                else
                {
                    usr.ExpensesList[si].Date = date_tb.Text;
                }
                date_value.Text = date_tb.Text;
                date_tb.Text = "New value";
                mw.update();
            }
            catch
            {
                //Error handling
            }
        }

        //This closes the current window
        private void done_Edit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}